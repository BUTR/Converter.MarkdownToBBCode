using HtmlAgilityPack;

using Markdig.Helpers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter.MarkdownToBBCodeNM;

// This code is not my proudest achievement. Thanks CommonMark!
// I regret nothing. Not proud, don't care. It works.

// The idea is to extract <p align="VALUE"></p> and convert to [center][/center], [left][/left] and [right][/right] BBCode
// Also convert <ins></ins> and <u></u> to [u][/y] BBCode
// Also convert <details><summary></summary></details> into [spoiler][/spoiler]

public class HtmlInlineRenderer : NexusModsObjectRenderer<HtmlInline>
{
    private HtmlInline? MatchingTagByNext(HtmlInline original, Markdig.Syntax.Inlines.Inline obj)
    {
        var current = obj;
        do
        {
            if (current is HtmlInline inl && inl.Tag.Trim('<', '>', '/') == original.Tag.Trim('<', '>')) return inl;
            if (current.NextSibling is not { } next) return null;
            current = next;
        } while (true);
    }

    // TODO: this one is still shite
    protected override void Write(NexusModsRenderer renderer, HtmlInline obj)
    {
        if (obj.Tag.StartsWith("</")) return;

        var current = obj.NextSibling;
        if (obj.NextSibling is null) return;
        var matching = MatchingTagByNext(obj, obj.NextSibling);
        if (matching is null) return;

        var sb = new StringBuilder();
        sb.Append(obj.Tag);
        var list = new List<Markdig.Syntax.Inlines.Inline> { obj };
        while (current != matching)
        {
            sb.Append(current switch
            {
                HtmlInline hi => hi.Tag,
                LiteralInline li => li.ToString(),
                LineBreakInline => HtmlUtils.NewLine,
                _ => throw new Exception(current?.GetType().ToString() ?? "NULL")
            });
            list.Add(current);
            current = current.NextSibling;
        }
        sb.Append(matching.Tag);
        list.Add(matching);
        foreach (var x in list.Skip(1))
        {
            x.Remove();
        }

        HtmlUtils.ProcessInline(renderer, sb.ToString());
    }
}

// Never had to use it
public class HtmlEntityInlineRenderer : NexusModsObjectRenderer<HtmlEntityInline>
{
    protected override void Write(NexusModsRenderer renderer, HtmlEntityInline obj)
    {
        renderer.Write(obj.Transcoded);
    }
}

public class HtmlBlockRenderer : NexusModsObjectRenderer<HtmlBlock>
{
    protected override void Write(NexusModsRenderer renderer, HtmlBlock obj)
    {
        HtmlUtils.Process(renderer, in obj.Lines);
    }
}

file static class HtmlUtils
{
    public static string NewLine => Environment.NewLine;

    private static string RemoveOneTabulationLevel(string html)
    {
        var lines = html.Split(NewLine);
        var firstEntry = lines.FirstOrDefault(x => x.Any(c => !char.IsWhiteSpace(c)));
        if (firstEntry is null) return html;
        var idx = firstEntry.Select((x, i) => (x, i)).FirstOrDefault(t => !char.IsWhiteSpace(t.x)).i;
        var tabulation = firstEntry.Substring(0, idx);
        return string.Join(NewLine, html.Split(NewLine).Select(x => x.StartsWith(tabulation) ? x.Substring(tabulation.Length) : x));
    }

    public static void ProcessInline(NexusModsRenderer renderer, string html)
    {
        if (html.Length == 0) return;

        var document = new HtmlDocument();
        document.LoadHtml(html);
        HandleHTML(renderer, document, true);
    }

    public static void Process(NexusModsRenderer renderer, in StringLineGroup group)
    {
        if (group.Lines.Length == 0) return;

        var isInline = group.Lines.Length == 1 || group.Lines[1].Slice.AsSpan().IsEmpty;

        var document = new HtmlDocument();
        document.LoadHtml(group.ToSlice().AsSpan().ToString());
        HandleHTML(renderer, document, isInline);
    }

    private static void HandleHTML(NexusModsRenderer renderer, HtmlDocument document, bool isInline)
    {
        foreach (var node in document.DocumentNode.ChildNodes.Where(x => x.NodeType != HtmlNodeType.Comment))
        {
            if (!HandleNode(renderer, node, isInline))
                renderer.Write(MarkdownNexusMods.ToBBCodeReuse(RemoveOneTabulationLevel(node.InnerHtml), false, false, renderer));
        }
    }
    private static bool HandleNode(NexusModsRenderer renderer, HtmlNode node, bool isInline)
    {
        if (node.Name == "p" && node.Attributes["align"] is { Value: { } pAlign })
        {
            WriteTag(renderer, isInline, false, pAlign, RemoveOneTabulationLevel(node.InnerHtml));
            return true;
        }
        if (node.Name == "img" && node.Attributes["src"] is { Value: { } impSrc })
        {
            WriteTag(renderer, isInline, false, "img", impSrc);
            return true;
        }
        if (node.Name == "ins" || node.Name == "u")
        {
            WriteTag(renderer, isInline, false, "u", RemoveOneTabulationLevel(node.InnerHtml));
            return true;
        }
        if (node.Name == "details")
        {
            if (node.ChildNodes.FirstOrDefault(x => x.Name == "summary") is { } summary)
                node.RemoveChild(summary);

            WriteTag(renderer, isInline, true, "spoiler", RemoveOneTabulationLevel(node.InnerHtml));
            return true;
        }

        return false;
    }

    private static void WriteTag(NexusModsRenderer renderer, bool isInline, bool forceNewLine, ReadOnlySpan<char> tag, ReadOnlySpan<char> content)
    {
        if (isInline)
        {
            renderer.Write($"[{tag}]");
            renderer.Write(MarkdownNexusMods.ToBBCodeReuse(content.ToString(), false, false, renderer));
            renderer.Write($"[/{tag}]");
        }
        else
        {
            if (renderer.HTMLForceNewLine || !renderer.IsFirstInContainer) renderer.EnsureLine();
            renderer.Write($"[{tag}]");
            if (content.StartsWith(NewLine)) renderer.EnsureLine();
            renderer.Write(MarkdownNexusMods.ToBBCodeReuse(content.ToString(), false, forceNewLine, renderer));
            if (content.EndsWith(NewLine)) renderer.EnsureLine();
            renderer.Write($"[/{tag}]");
            if (renderer.HTMLForceNewLine || !renderer.IsLastInContainer) renderer.EnsureLine();
        }
    }
}