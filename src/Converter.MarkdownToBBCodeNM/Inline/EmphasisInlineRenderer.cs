using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCodeNM.Inline;

public class EmphasisInlineRenderer : NexusModsObjectRenderer<EmphasisInline>
{
    protected override void Write(NexusModsRenderer renderer, EmphasisInline obj)
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
            case { DelimiterChar: '~', DelimiterCount: 2 }:
                renderer.Write("[s]");
                renderer.WriteChildren(obj);
                renderer.Write("[/s]");
                break;
            default:
                renderer.WriteChildren(obj);
                break;
        }
    }
}