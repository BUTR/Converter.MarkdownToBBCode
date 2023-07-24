using HtmlAgilityPack;

using Markdig.Parsers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Converter.MarkdownToBBCode.Shared.Html;

// This code is not my proudest achievement. Thanks CommonMark!
// I regret nothing. It works.

// The idea is to extract <p align="VALUE"></p> and convert to [center][/center], [left][/left] and [right][/right] BBCode
// Also convert <ins></ins> and <u></u> to [u][/y] BBCode
// Also convert <details><summary></summary></details> into [spoiler][/spoiler]

internal static class HtmlUtils
{
    private static string NewLine => Environment.NewLine;

    private static string ToBBCodeReuse(string markdown, bool? doubleLineBreakAsNewLine, bool htmlForceNewLine, BBCodeRenderer rendererOld)
    {
        var document = MarkdownParser.Parse(markdown.Trim('\r', '\n'), rendererOld.Pipeline);

        using var writer = new StringWriter();
        var renderer = new BBCodeRenderer(rendererOld.BBCodeType, rendererOld.Pipeline, doubleLineBreakAsNewLine ?? rendererOld.DoubleLineBreakAsNewLine, rendererOld.HandleHTML, writer)
        {
            HTMLForceNewLine = htmlForceNewLine
        };
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }

    private static string RemoveOneTabulationLevel(string html)
    {
        var lines = html.Split(NewLine);
        var firstEntry = lines.FirstOrDefault(x => x.Any(c => !char.IsWhiteSpace(c)));
        if (firstEntry is null) return html;
        var idx = firstEntry.Select((x, i) => (x, i)).FirstOrDefault(t => !char.IsWhiteSpace(t.x)).i;
        var tabulation = firstEntry.Substring(0, idx);
        return string.Join(NewLine, html.Split(NewLine).Select(x => x.StartsWith(tabulation) ? x.Substring(tabulation.Length) : x));
    }

    public static void ProcessLeafBlock(BBCodeRenderer renderer, LeafBlock obj)
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
            do
            {
                sb.Append(current switch
                {
                    HtmlInline hi => hi.Tag,
                    HtmlEntityInline hei => hei.Transcoded,
                    LiteralInline li => li.ToString(),
                    LineBreakInline => NewLine,
                    //LineBreakInline => "</br>",
                    _ => throw new Exception(current?.GetType().ToString() ?? "NULL")
                });
                if (current == htmlInlineEnd) break;
                current = current.NextSibling;
            } while (true);

            // Write and remove anything before the HTML inlining
            var start = obj.Inline.FirstChild;
            while (start != htmlInlineStart)
            {
                var temp = start;
                renderer.Write(start);
                start = start.NextSibling;
                temp.Remove();
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

    public static void ProcessHTMLDocument(BBCodeRenderer renderer, HtmlDocument document, bool isInline)
    {
        foreach (var node in document.DocumentNode.ChildNodes.Where(x => x.NodeType != HtmlNodeType.Comment))
        {
            if (!ProcessHTMLNode(renderer, node, isInline))
                renderer.Write(ToBBCodeReuse(RemoveOneTabulationLevel(node.InnerHtml), false, false, renderer));
        }
    }
    private static bool ProcessHTMLNode(BBCodeRenderer renderer, HtmlNode node, bool isInline)
    {
        if (node.Attributes["converter_ignore"] is not null) return true;
        switch (node.Name)
        {
            case "br":
                renderer.EnsureLine();
                return true;
            case "b":
                WriteBBCode(renderer, isInline, true, false, "b", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "i":
                WriteBBCode(renderer, isInline, true, false, "i", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "ins" or "u":
                WriteBBCode(renderer, isInline, true, false, "u", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "s" or "strike":
                WriteBBCode(renderer, isInline, true, false, "s", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "a" when node.Attributes["nexusmods_href"] is { Value: { } href } && renderer.BBCodeType == BBCodeType.NexusMods:
                WriteBBCode(renderer, isInline, true, false, "url", $"={href}", RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "a" when node.Attributes["steam_href"] is { Value: { } href } && renderer.BBCodeType == BBCodeType.Steam:
                WriteBBCode(renderer, isInline, true, false, "url", $"={href}", RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "a" when node.Attributes["href"] is { Value: { } href }:
                WriteBBCode(renderer, isInline, true, false, "url", $"={href}", RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "img" when node.Attributes["nexusmods_src"] is { Value: { } src } && renderer.BBCodeType == BBCodeType.NexusMods:
                WriteBBCode(renderer, isInline, true, false, "img", ReadOnlySpan<char>.Empty, src);
                return true;
            case "img" when node.Attributes["steam_src"] is { Value: { } src } && renderer.BBCodeType == BBCodeType.Steam:
                WriteBBCode(renderer, isInline, true, false, "img", ReadOnlySpan<char>.Empty, src);
                return true;
            case "img" when node.Attributes["src"] is { Value: { } src }:
                WriteBBCode(renderer, isInline, true, false, "img", ReadOnlySpan<char>.Empty, src);
                return true;
            case "blockquote":
                WriteBBCode(renderer, isInline, true, true, "quote", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "code":
                WriteBBCode(renderer, isInline, true, true, "code", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "ol" when renderer.BBCodeType == BBCodeType.NexusMods:
                WriteBBCode(renderer, isInline, true, true, "list", "=1", RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "ol" when renderer.BBCodeType == BBCodeType.Steam:
                WriteBBCode(renderer, isInline, true, true, "olist", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "ul":
                WriteBBCode(renderer, isInline, true, true, "list", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "li":
                WriteBBCode(renderer, isInline, false, true, "*", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "hr" when renderer.BBCodeType == BBCodeType.NexusMods:
                if (!isInline) renderer.EnsureLine();
                WriteBBCode(renderer, isInline, false, false, "line", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                if (!isInline) renderer.EnsureLine();
                return true;
            case "hr" when renderer.BBCodeType == BBCodeType.Steam:
                if (!isInline) renderer.EnsureLine();
                WriteBBCode(renderer, isInline, true, false, "hr", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                if (!isInline) renderer.EnsureLine();
                return true;
            case "p" or "div" when node.Attributes["align"] is { Value: { } align } && renderer.BBCodeType == BBCodeType.NexusMods:
                WriteBBCode(renderer, isInline, true, false, align, ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case ['h', var d] when char.IsDigit(d) && renderer.BBCodeType == BBCodeType.NexusMods:
                if (!isInline) renderer.EnsureLine();
                WriteBBCode(renderer, isInline, true, true, "size", $"={((int) (7 - char.GetNumericValue(d)))}", RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case ['h', var d] when char.IsDigit(d) && renderer.BBCodeType == BBCodeType.Steam:
                if (!isInline) renderer.EnsureLine();
                WriteBBCode(renderer, isInline, true, true, $"h{d}", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            case "details" when renderer.BBCodeType == BBCodeType.NexusMods:
                WriteBBCode(renderer, isInline, true, true, "spoiler", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
            // Only inline spoilers are supported by Steam
            case "details" when renderer.BBCodeType == BBCodeType.Steam:
                if (!isInline && (renderer.HTMLForceNewLine || !renderer.IsFirstInContainer)) renderer.EnsureLine();
                renderer.Write(ToBBCodeReuse(RemoveOneTabulationLevel(node.InnerHtml), false, true, renderer));
                if (!isInline && (renderer.HTMLForceNewLine || !renderer.IsLastInContainer)) renderer.EnsureLine();
                return true;
            case "summary":
                WriteBBCode(renderer, isInline, true, false, "b", ReadOnlySpan<char>.Empty, RemoveOneTabulationLevel(node.InnerHtml));
                return true;
        }
        return false;
    }

    private static void WriteBBCode(BBCodeRenderer renderer, bool isInline, bool closeTag, bool forceNewLine, ReadOnlySpan<char> tag, ReadOnlySpan<char> additional, ReadOnlySpan<char> content)
    {
        if (!isInline && (renderer.HTMLForceNewLine || !renderer.IsFirstInContainer)) renderer.EnsureLine();
        renderer.Write($"[{tag}{additional}]");
        if (!isInline && content.StartsWith(NewLine)) renderer.EnsureLine();
        renderer.Write(ToBBCodeReuse(content.ToString(), false, !isInline && forceNewLine, renderer));
        if (!isInline && content.EndsWith(NewLine)) renderer.EnsureLine();
        if (closeTag) renderer.Write($"[/{tag}]");
        if (!isInline && (renderer.HTMLForceNewLine || !renderer.IsLastInContainer)) renderer.EnsureLine();
    }
}