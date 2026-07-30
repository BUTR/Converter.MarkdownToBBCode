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
            // Steam only renders [h1]-[h3], deeper levels fall back to bold
            case BBCodeType.Steam when obj.Level <= 3:
                renderer.Write($"[h{obj.Level}]");
                renderer.WriteLeafInline(obj);
                renderer.Write($"[/h{obj.Level}]");
                break;
            case BBCodeType.Steam:
                renderer.Write("[b]");
                renderer.WriteLeafInline(obj);
                renderer.Write("[/b]");
                break;
        }

        renderer.WriteLinesEnd(obj);
    }
}