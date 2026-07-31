using HtmlAgilityPack;

using Markdig.Helpers;
using Markdig.Syntax;

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Converter.MarkdownToBBCode.Shared.Html;

public class HtmlBlockRenderer : BBCodeObjectRenderer<HtmlBlock>
{
    // Block-level container tags that produce a wrapping BBCode tag and can legally
    // span multiple paragraphs, so a blank line inside them must not break the wrapping
    private static readonly string[] StitchableTags = { "details", "p", "div", "blockquote" };

    private static string StripSelfClosing(string html) => Regex.Replace(html, @"<[^>]*/>", string.Empty);

    // Counts opening (</=false) or closing (</=true) occurrences of a tag, requiring a
    // tag-name boundary so "<p" does not match "<pre"
    private static int CountTags(string html, string tag, bool closing)
    {
        var needle = closing ? "</" + tag : "<" + tag;
        var count = 0;
        var idx = 0;
        while ((idx = html.IndexOf(needle, idx, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            var after = idx + needle.Length;
            if (after >= html.Length || html[after] is ' ' or '\t' or '\r' or '\n' or '>' or '/') count++;
            idx = after;
        }
        return count;
    }

    private static int Depth(string html, string tag)
    {
        var clean = StripSelfClosing(html);
        return CountTags(clean, tag, false) - CountTags(clean, tag, true);
    }

    // A block-level element followed by a blank line is split by CommonMark into multiple
    // blocks, leaving the element unclosed. Stitch the original source back together so the
    // whole element is converted as one (issue #18, and <p align="center"> wrapping).
    private static bool TryProcessSplitElement(BBCodeRenderer renderer, HtmlBlock obj, string html)
    {
        if (renderer.Source is not { } source || obj.Parent is not { } parent) return false;

        var clean = StripSelfClosing(html);
        var tag = StitchableTags
            .Where(t => Depth(html, t) > 0)
            .OrderBy(t => clean.IndexOf("<" + t, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();
        if (tag is null) return false;

        var depth = Depth(html, tag);
        HtmlBlock? closing = null;
        for (var i = parent.IndexOf(obj) + 1; i < parent.Count && closing is null; i++)
        {
            if (parent[i] is not HtmlBlock nextHtml) continue;
            depth += Depth(nextHtml.Lines.ToSlice().AsSpan().ToString(), tag);
            if (depth <= 0) closing = nextHtml;
        }
        if (closing is null || closing.Span.End >= source.Length || closing.Span.End <= obj.Span.Start) return false;

        var document = new HtmlDocument();
        document.LoadHtml(source.Substring(obj.Span.Start, closing.Span.End - obj.Span.Start + 1));
        if (!HtmlUtils.CanProcess(renderer, document)) return false;

        // Consume the blocks that got stitched in so they are not rendered twice
        for (var i = parent.IndexOf(obj) + 1; i < parent.Count;)
        {
            var next = parent[i];
            parent.RemoveAt(i);
            if (next == closing) break;
        }

        renderer.WriteLinesStart(obj);
        HtmlUtils.ProcessHTMLDocument(renderer, document, false);
        renderer.WriteLinesEnd(obj);
        return true;
    }

    protected override void Write(BBCodeRenderer renderer, HtmlBlock obj)
    {
        if (obj.Lines.Lines.Length == 0) return;

        var html = obj.Lines.ToSlice().AsSpan().ToString();

        if (TryProcessSplitElement(renderer, obj, html)) return;

        var document = new HtmlDocument();
        document.LoadHtml(html);

        if (!HtmlUtils.CanProcess(renderer, document))
        {
            if (!renderer.IsLastInContainer) renderer.EnsureLine();
            if (renderer.IsLastInContainer && obj.LinesAfter?.Count > 0 && obj.LinesAfter?[0].NewLine != NewLine.None) renderer.WriteLine();
        }
        else
        {
            renderer.WriteLinesStart(obj);
            HtmlUtils.ProcessHTMLDocument(renderer, document, false);
            renderer.WriteLinesEnd(obj);
        }
    }
}