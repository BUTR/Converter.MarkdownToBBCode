using HtmlAgilityPack;

using Markdig.Helpers;
using Markdig.Syntax;

using System;

namespace Converter.MarkdownToBBCode.Shared.Html;

public class HtmlBlockRenderer : BBCodeObjectRenderer<HtmlBlock>
{
    private static int CountTags(string html, string tag)
    {
        var count = 0;
        var idx = 0;
        while ((idx = html.IndexOf(tag, idx, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            idx += tag.Length;
        }
        return count;
    }

    // An HTML element followed by a blank line is split by CommonMark into multiple blocks,
    // leaving the element unclosed. Stitch the original source back together so the whole
    // <details> element is converted as one (issue #18)
    private static bool TryProcessSplitDetails(BBCodeRenderer renderer, HtmlBlock obj, string html)
    {
        if (renderer.Source is not { } source || obj.Parent is not { } parent) return false;
        if (CountTags(html, "<details") <= CountTags(html, "</details")) return false;

        HtmlBlock? closing = null;
        for (var i = parent.IndexOf(obj) + 1; i < parent.Count; i++)
        {
            if (parent[i] is HtmlBlock nextHtml && CountTags(nextHtml.Lines.ToSlice().AsSpan().ToString(), "</details") > 0)
            {
                closing = nextHtml;
                break;
            }
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

        if (TryProcessSplitDetails(renderer, obj, html)) return;

        var document = new HtmlDocument();
        document.LoadHtml(html);

        if (!HtmlUtils.CanProcess(renderer, document))
        {
            var idx = obj.Parent?.IndexOf(obj);
            var nextElement = idx is null or <= 0 ? null : obj.Parent?[idx.Value - 1];
            var isHtml = nextElement is HtmlBlock;
            var isRenderedHtml = nextElement is HtmlBlock htmlBlock && HtmlUtils.CanProcess(renderer, htmlBlock.Lines.ToSlice().AsSpan().ToString());

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