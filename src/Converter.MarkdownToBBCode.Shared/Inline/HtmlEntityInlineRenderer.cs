using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCode.Shared.Inline;

// BBCode has no entity support, so always emit the transcoded character (&amp; -> &).
// Entities inside an HTML inline run are consumed by HtmlUtils.ProcessLeafBlock instead;
// this covers entities in plain text (e.g. "Tom &amp; Jerry"), which were dropped before.
public class HtmlEntityInlineRenderer : BBCodeObjectRenderer<HtmlEntityInline>
{
    protected override void Write(BBCodeRenderer renderer, HtmlEntityInline obj)
    {
        renderer.Write(obj.Transcoded.AsSpan());
    }
}