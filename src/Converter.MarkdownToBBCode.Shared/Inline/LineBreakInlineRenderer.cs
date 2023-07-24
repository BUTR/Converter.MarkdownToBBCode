using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCode.Shared.Inline;

public class LineBreakInlineRenderer : BBCodeObjectRenderer<LineBreakInline>
{
    protected override void Write(BBCodeRenderer renderer, LineBreakInline obj)
    {
        if (!renderer.IsLastInContainer) renderer.EnsureLine();
    }
}