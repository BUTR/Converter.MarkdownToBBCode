using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCodeNM.Inline;

public class LiteralInlineRenderer : NexusModsObjectRenderer<LiteralInline>
{
    protected override void Write(NexusModsRenderer renderer, LiteralInline obj)
    {
        renderer.Write(ref obj.Content);
    }
}