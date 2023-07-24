using Markdig.Helpers;
using Markdig.Syntax;

namespace Converter.MarkdownToBBCode.Shared;

public class HeadingRenderer : BBCodeObjectRenderer<HeadingBlock>
{
    protected override void Write(BBCodeRenderer renderer, HeadingBlock obj)
    {
        if (obj.LinesBefore?.Count > 0 && obj.LinesBefore?[0].NewLine != NewLine.None) renderer.WriteLine();
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();

        switch (renderer.BBCodeType)
        {
            case BBCodeType.NexusMods:
                renderer.Write($"[size={7 - obj.Level}]");
                renderer.WriteLeafInline(obj);
                renderer.Write("[/size]");
                break;
            case BBCodeType.Steam:
                renderer.Write($"[h{obj.Level}]");
                renderer.WriteLeafInline(obj);
                renderer.Write($"[/h{obj.Level}]");
                break;
        }

        if (!renderer.IsLastInContainer) renderer.EnsureLine();
        if (obj.LinesAfter?.Count > 0 && obj.LinesAfter?[0].NewLine != NewLine.None) renderer.WriteLine();
    }
}