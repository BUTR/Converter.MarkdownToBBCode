using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCodeNM.Inline;

public class LineBreakInlineRenderer : NexusModsObjectRenderer<LineBreakInline>
{
    protected override void Write(NexusModsRenderer renderer, LineBreakInline obj)
    {
        if (!renderer.IsLastInContainer) renderer.EnsureLine();
    }
}