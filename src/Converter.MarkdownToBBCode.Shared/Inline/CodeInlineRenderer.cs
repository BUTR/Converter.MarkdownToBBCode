using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCode.Shared.Inline;

public class CodeInlineRenderer : BBCodeObjectRenderer<CodeInline>
{
    protected override void Write(BBCodeRenderer renderer, CodeInline obj)
    {
        // Neither platform has an inline monospace tag ([code] renders as a block box),
        // so inline code falls back to bold. Steam additionally has [noparse], which keeps
        // literal [tags] inside the code span from being interpreted as BBCode.
        switch (renderer.BBCodeType)
        {
            case BBCodeType.Steam:
                renderer.Write("[b][noparse]");
                renderer.Write(obj.ContentSpan);
                renderer.Write("[/noparse][/b]");
                break;
            default:
                renderer.Write("[b]");
                renderer.Write(obj.ContentSpan);
                renderer.Write("[/b]");
                break;
        }
    }
}
