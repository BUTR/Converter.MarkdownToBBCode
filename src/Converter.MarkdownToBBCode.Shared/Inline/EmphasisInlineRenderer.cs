using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCode.Shared.Inline;

public class EmphasisInlineRenderer : BBCodeObjectRenderer<EmphasisInline>
{
    protected override void Write(BBCodeRenderer renderer, EmphasisInline obj)
    {
        switch (obj)
        {
            case { DelimiterChar: '*' or '_', DelimiterCount: 2 }:
                renderer.Write("[b]");
                renderer.WriteChildren(obj);
                renderer.Write("[/b]");
                break;
            case { DelimiterChar: '*' or '_', DelimiterCount: 1 }:
                renderer.Write("[i]");
                renderer.WriteChildren(obj);
                renderer.Write("[/i]");
                break;
            case { DelimiterChar: '~', DelimiterCount: 2 } when renderer.BBCodeType is BBCodeType.NexusMods:
                renderer.Write("[s]");
                renderer.WriteChildren(obj);
                renderer.Write("[/s]");
                break;
            case { DelimiterChar: '~', DelimiterCount: 2 } when renderer.BBCodeType is BBCodeType.Steam:
                renderer.Write("[strike]");
                renderer.WriteChildren(obj);
                renderer.Write("[/strike]");
                break;
            default:
                renderer.WriteChildren(obj);
                break;
        }
    }
}