using HtmlAgilityPack;

using Markdig.Syntax;

namespace Converter.MarkdownToBBCode.Shared.Html;

public class HtmlBlockRenderer : BBCodeObjectRenderer<HtmlBlock>
{
    protected override void Write(BBCodeRenderer renderer, HtmlBlock obj)
    {
        if (obj.Lines.Lines.Length == 0) return;

        var isInline = obj.Lines.Lines.Length == 1 || obj.Lines.Lines[1].Slice.AsSpan().IsEmpty;

        var document = new HtmlDocument();
        document.LoadHtml(obj.Lines.ToSlice().AsSpan().ToString());
        HtmlUtils.ProcessHTMLDocument(renderer, document, isInline);
    }
}