using Markdig.Syntax;

namespace Converter.MarkdownToBBCode.Shared;

public class HeadingRenderer : BBCodeObjectRenderer<HeadingBlock>
{
    protected override void Write(BBCodeRenderer renderer, HeadingBlock obj)
    {
        renderer.WriteLinesStart(obj);

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

        renderer.WriteLinesEnd(obj);
    }
}