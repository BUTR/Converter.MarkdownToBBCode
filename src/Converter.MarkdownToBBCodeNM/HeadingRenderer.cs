using Markdig.Helpers;
using Markdig.Syntax;

namespace Converter.MarkdownToBBCodeNM;

public class HeadingRenderer : NexusModsObjectRenderer<HeadingBlock>
{
    protected override void Write(NexusModsRenderer renderer, HeadingBlock obj)
    {
        if (obj.LinesBefore?.Count > 0 && obj.LinesBefore?[0].NewLine != NewLine.None) renderer.WriteLine();
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();

        renderer.Write($"[size={7 - obj.Level}]");
        renderer.WriteLeafInline(obj);
        renderer.Write("[/size]");

        if (!renderer.IsLastInContainer) renderer.EnsureLine();
        if (obj.LinesAfter?.Count > 0 && obj.LinesAfter?[0].NewLine != NewLine.None) renderer.WriteLine();
    }
}