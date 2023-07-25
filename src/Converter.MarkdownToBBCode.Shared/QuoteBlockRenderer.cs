using Markdig.Helpers;
using Markdig.Syntax;

namespace Converter.MarkdownToBBCode.Shared;

public class QuoteBlockRenderer : BBCodeObjectRenderer<QuoteBlock>
{
    protected override void Write(BBCodeRenderer renderer, QuoteBlock obj)
    {
        renderer.WriteLinesStart(obj);

        renderer.Write("[quote]");
        renderer.EnsureLine();
        renderer.WriteChildren(obj);
        renderer.EnsureLine();
        renderer.Write("[/quote]");

        renderer.WriteLinesEnd(obj);
    }
}