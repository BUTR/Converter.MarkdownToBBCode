using Markdig.Syntax;

namespace Converter.MarkdownToBBCode.Shared;

// Renders Markdown thematic breaks (---, ***, ___) the same way <hr> HTML is rendered
public class ThematicBreakRenderer : BBCodeObjectRenderer<ThematicBreakBlock>
{
    protected override void Write(BBCodeRenderer renderer, ThematicBreakBlock obj)
    {
        renderer.WriteLinesStart(obj);

        switch (renderer.BBCodeType)
        {
            case BBCodeType.NexusMods:
                renderer.Write("[line]");
                break;
            case BBCodeType.Steam:
                renderer.Write("[hr][/hr]");
                break;
        }

        renderer.WriteLinesEnd(obj);
    }
}
