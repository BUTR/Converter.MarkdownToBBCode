using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCode.Shared.Inline;

public class CodeInlineRenderer : BBCodeObjectRenderer<CodeInline>
{
    protected override void Write(BBCodeRenderer renderer, CodeInline obj)
    {
        renderer.Write("[b]");
        renderer.Write(obj.ContentSpan);
        renderer.Write("[/b]");
    }
}
