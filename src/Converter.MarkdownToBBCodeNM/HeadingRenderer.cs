using Markdig.Syntax;

namespace Converter.MarkdownToBBCodeNM;

public class HeadingRenderer : NexusModsObjectRenderer<HeadingBlock>
{
    protected override void Write(NexusModsRenderer renderer, HeadingBlock obj)
    {
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();
        if (!renderer.IsFirstInContainer) renderer.WriteLine();
        renderer.Write($"[size={7 - obj.Level}]");
        renderer.WriteLeafInline(obj);
        renderer.Write("[/size]");
        if (!renderer.IsLastInContainer) renderer.EnsureLine();
    }
}