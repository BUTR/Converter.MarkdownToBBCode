using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCode.Shared.Inline;

public class CodeInlineRenderer : BBCodeObjectRenderer<CodeInline>
{
    protected override void Write(BBCodeRenderer renderer, CodeInline obj)
    {
        // Neither platform has an inline monospace tag ([code] renders as a block box),
        // so inline code falls back to bold. Content is deliberately NOT escaped: Steam
        // could use [noparse], but NexusMods has no equivalent, and protecting only one
        // platform would mask breakage on the other while sharing a single source.
        renderer.Write("[b]");
        renderer.Write(obj.ContentSpan);
        renderer.Write("[/b]");
    }
}
