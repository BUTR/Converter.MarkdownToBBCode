using HtmlAgilityPack;

using Markdig.Helpers;
using Markdig.Syntax;

namespace Converter.MarkdownToBBCode.Shared.Html;

public class HtmlBlockRenderer : BBCodeObjectRenderer<HtmlBlock>
{
    protected override void Write(BBCodeRenderer renderer, HtmlBlock obj)
    {
        if (obj.Lines.Lines.Length == 0) return;

        var document = new HtmlDocument();
        document.LoadHtml(obj.Lines.ToSlice().AsSpan().ToString());

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