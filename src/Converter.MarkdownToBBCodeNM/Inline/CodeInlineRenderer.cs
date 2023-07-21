using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCodeNM.Inline;

public class CodeInlineRenderer : NexusModsObjectRenderer<CodeInline>
{
    protected override void Write(NexusModsRenderer renderer, CodeInline obj)
    {
        renderer.Write("[b]");
        renderer.Write(obj.ContentSpan);
        renderer.Write("[/b]");

        /* NexusMods can't render inline code blocks
        renderer.Write("[code]");
        renderer.Write(obj.ContentSpan);
        renderer.Write("[/code]");
        */
    }
}