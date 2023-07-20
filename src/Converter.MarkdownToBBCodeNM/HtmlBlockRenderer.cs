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

    protected override void Write(NexusModsRenderer renderer, HtmlInline obj)
    {
        var current = obj.NextSibling;
        if (obj.NextSibling is null) return;
        var matching = MatchingTagByNext(obj, obj.NextSibling);

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
        list.Add(current);
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

    private const string AlignStart = "<p align=";
    private const string InsStart = "<ins";
    private const string UStart = "<u";
    private const string SpoilerStart = "<details";

    public static void ProcessInline(NexusModsRenderer renderer, string html)
    {
        if (html.Length == 0) return;

        var span = html.AsSpan();
        var contentStart = span.IndexOf('>') + 1;
        var contentLength = span.Slice(contentStart).LastIndexOf("</");
        var content = span.Slice(contentStart, contentLength == -1 ? span.Length : contentLength);

        var alignValue = GetAlign(html);
        if (!alignValue.IsEmpty)
        {
            WriteTag(renderer, true, alignValue, content);
            return;
        }

        if (IsUnderscore(html))
        {
            WriteTag(renderer, true, "u", content);
            return;
        }

        if (IsSpoiler(html))
        {
            const string summaryEnd = "</summary>";
            var idxEnd = content.IndexOf(summaryEnd);

            // Don't handle markdown's line breaks
            WriteTag(renderer, true, "spoiler", content.Slice(idxEnd + summaryEnd.Length).TrimStart(NewLine));
            return;
        }

        renderer.Write(MarkdownNexusMods.ToBBCodeReuse(content.ToString(), false, renderer));
    }

    public static void Process(NexusModsRenderer renderer, in StringLineGroup group)
    {
        if (group.Lines.Length == 0) return;

        var isInline = group.Lines.Length == 1 || group.Lines[1].Slice.AsSpan().IsEmpty;

        var span = isInline ? group.Lines[0].Slice.AsSpan() : group.ToSlice().AsSpan();
        var contentStart = span.IndexOf('>') + 1;
        var contentLength = span.Slice(contentStart).LastIndexOf("</");
        var content = span.Slice(contentStart, contentLength == -1 ? span.Length : contentLength);

        var alignValue = GetAlign(group.Lines);
        if (!alignValue.IsEmpty)
        {
            WriteTag(renderer, isInline, alignValue, content);
            return;
        }

        if (IsUnderscore(group.Lines))
        {
            WriteTag(renderer, isInline, "u", content);
            return;
        }

        if (IsSpoiler(group.Lines))
        {
            const string summaryEnd = "</summary>";
            var idxEnd = content.IndexOf(summaryEnd);

            // Don't handle markdown's line breaks
            WriteTag(renderer, isInline, "spoiler", content.Slice(idxEnd + summaryEnd.Length));
            return;
        }

        renderer.Write(MarkdownNexusMods.ToBBCodeReuse(content.ToString(), false, renderer));
    }

    private static void WriteTag(NexusModsRenderer renderer, bool isInline, ReadOnlySpan<char> tag, ReadOnlySpan<char> content)
    {
        if (isInline)
        {
            renderer.Write($"[{tag}]");
            renderer.Write(MarkdownNexusMods.ToBBCodeReuse(content.ToString(), false, renderer));
            renderer.Write($"[/{tag}]");
        }
        else
        {
            if (!renderer.IsFirstInContainer) renderer.EnsureLine();
            renderer.Write($"[{tag}]");
            if (content.StartsWith(NewLine)) renderer.EnsureLine();
            // I do the whitespace trimming because the spoiler will be then marked as a code block
            renderer.Write(MarkdownNexusMods.ToBBCodeReuse(content.Trim(NewLine).Trim().ToString(), false, renderer));
            if (content.EndsWith(NewLine)) renderer.EnsureLine();
            renderer.Write($"[/{tag}]");
            if (!renderer.IsLastInContainer) renderer.EnsureLine();
        }
    }

    private static ReadOnlySpan<char> GetAlign(in StringLine[] lines)
    {
        ref var slice = ref lines[0].Slice;
        return GetAlign(slice.AsSpan());
    }
    private static ReadOnlySpan<char> GetAlign(ReadOnlySpan<char> span)
    {
        if (span.StartsWith(AlignStart))
        {
            var alignStartSlice = span.Slice(AlignStart.Length + 1);
            if (alignStartSlice.IndexOf('"') is var idx && idx != -1)
                return alignStartSlice.Slice(0, idx);
        }
        return ReadOnlySpan<char>.Empty;
    }

    private static bool IsUnderscore(in StringLine[] lines)
    {
        ref var slice = ref lines[0].Slice;
        return IsUnderscore(slice.AsSpan());
    }
    private static bool IsUnderscore(ReadOnlySpan<char> span)
    {
        return span.StartsWith(InsStart) || span.StartsWith(UStart);
    }

    private static bool IsSpoiler(in StringLine[] lines)
    {
        ref var slice = ref lines[0].Slice;
        return IsSpoiler(slice.AsSpan());
    }
    private static bool IsSpoiler(ReadOnlySpan<char> span)
    {
        return span.StartsWith(SpoilerStart);
    }
}