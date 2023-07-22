using HtmlAgilityPack;

using Markdig.Syntax;
using Markdig.Syntax.Inlines;

using System;
using System.Linq;
using System.Text;

namespace Converter.MarkdownToBBCodeNM;

// This code is not my proudest achievement. Thanks CommonMark!
// I regret nothing. It works.

// The idea is to extract <p align="VALUE"></p> and convert to [center][/center], [left][/left] and [right][/right] BBCode
// Also convert <ins></ins> and <u></u> to [u][/y] BBCode
// Also convert <details><summary></summary></details> into [spoiler][/spoiler]

internal static class HtmlUtils
{
    private static string NewLine => Environment.NewLine;

    private static string RemoveOneTabulationLevel(string html)
    {
        var lines = html.Split(NewLine);
        var firstEntry = lines.FirstOrDefault(x => x.Any(c => !char.IsWhiteSpace(c)));
        if (firstEntry is null) return html;
        var idx = firstEntry.Select((x, i) => (x, i)).FirstOrDefault(t => !char.IsWhiteSpace(t.x)).i;
        var tabulation = firstEntry.Substring(0, idx);
        return string.Join(NewLine, html.Split(NewLine).Select(x => x.StartsWith(tabulation) ? x.Substring(tabulation.Length) : x));
    }

    public static void ProcessLeafBlock(NexusModsRenderer renderer, LeafBlock obj)
    {
        static HtmlInline? FirstHtmlInline(Markdig.Syntax.Inlines.Inline? start)
        {
            if (start is null) return null;
            if (start is HtmlInline) return (HtmlInline) start;

            var current = start;
            while (current.NextSibling is { } next)
            {
                if (next is HtmlInline htmlInline) return htmlInline;
                current = next;
            }
            return null;
        }

        static HtmlInline LastHtmlInline(HtmlInline start)
        {
            Markdig.Syntax.Inlines.Inline current = start;
            while (current.NextSibling is { } next)
            {
                current = next;
            }
            if (current is HtmlInline) return (HtmlInline) current;
            while (current.PreviousSibling is { } prev)
            {
                if (prev is HtmlInline htmlInline) return htmlInline;
                current = prev;
            }
            return start;
        }

        if (FirstHtmlInline(obj.Inline?.FirstChild) is { } htmlInlineStart)
        {
            var htmlInlineEnd = LastHtmlInline(htmlInlineStart);

            var sb = new StringBuilder();

            Markdig.Syntax.Inlines.Inline? current = htmlInlineStart;
            while (current != htmlInlineEnd)
            {
                sb.Append(current switch
                {
                    HtmlInline hi => hi.Tag,
                    LiteralInline li => li.ToString(),
                    LineBreakInline => NewLine,
                    //LineBreakInline => "</br>",
                    _ => throw new Exception(current?.GetType().ToString() ?? "NULL")
                });
                current = current.NextSibling;
            }
            current = htmlInlineStart;
            while (current != htmlInlineEnd)
            {
                var temp = current;
                current = current!.NextSibling;
                temp!.Remove();
            }
            htmlInlineEnd.Remove();

            var document = new HtmlDocument();
            document.LoadHtml(sb.ToString());
            ProcessHTMLDocument(renderer, document, true);
        }
    }

    public static void ProcessHTMLDocument(NexusModsRenderer renderer, HtmlDocument document, bool isInline)
    {
        foreach (var node in document.DocumentNode.ChildNodes.Where(x => x.NodeType != HtmlNodeType.Comment))
        {
            if (!ProcessHTMLNode(renderer, node, isInline))
                renderer.Write(MarkdownNexusMods.ToBBCodeReuse(RemoveOneTabulationLevel(node.InnerHtml), false, false, renderer));
        }
    }
    private static bool ProcessHTMLNode(NexusModsRenderer renderer, HtmlNode node, bool isInline)
    {
        if (node.Attributes["converter_ignore"] is not null) return true;
        switch (node.Name)
        {
            case "p" when node.Attributes["align"] is { Value: { } align }:
                WriteBBCode(renderer, isInline, true, false, align, ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "a" when node.Attributes["nexusmods_href"] is { Value: { } href }:
                WriteBBCode(renderer, isInline, true, false, "url", $"={href}", RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "a" when node.Attributes["href"] is { Value: { } href }:
                WriteBBCode(renderer, isInline, true, false, "url", $"={href}", RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "img" when node.Attributes["nexusmods_src"] is { Value: { } src }:
                WriteBBCode(renderer, isInline, true, false, "img", ReadOnlySpan<char>.Empty, src);
                return true;
            case "img" when node.Attributes["src"] is { Value: { } src }:
                WriteBBCode(renderer, isInline, true, false, "img", ReadOnlySpan<char>.Empty, src);
                return true;
            case "ins":
            case "u":
                WriteBBCode(renderer, isInline, true, false, "u", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "hr":
                renderer.EnsureLine();
                WriteBBCode(renderer, isInline, false, false, "line", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                renderer.EnsureLine();
                return true;
            case "br":
                renderer.EnsureLine();
                return true;
            case "details":
                WriteBBCode(renderer, isInline, true, true, "spoiler", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "summary":
                WriteBBCode(renderer, isInline, true, false, "b", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
        }
        return false;
    }

    private static void WriteBBCode(NexusModsRenderer renderer, bool isInline, bool closeTag, bool forceNewLine, ReadOnlySpan<char> tag, ReadOnlySpan<char> additional, ReadOnlySpan<char> content)
    {
        if (isInline)
        {
            renderer.Write($"[{tag}{additional}]");
            renderer.Write(MarkdownNexusMods.ToBBCodeReuse(content.ToString(), false, false, renderer));
            if (closeTag) renderer.Write($"[/{tag}]");
        }
        else
        {
            if (renderer.HTMLForceNewLine || !renderer.IsFirstInContainer) renderer.EnsureLine();
            renderer.Write($"[{tag}{additional}]");
            if (content.StartsWith(NewLine)) renderer.EnsureLine();
            renderer.Write(MarkdownNexusMods.ToBBCodeReuse(content.ToString(), false, forceNewLine, renderer));
            if (content.EndsWith(NewLine)) renderer.EnsureLine();
            if (closeTag) renderer.Write($"[/{tag}]");
            if (renderer.HTMLForceNewLine || !renderer.IsLastInContainer) renderer.EnsureLine();
        }
    }
}