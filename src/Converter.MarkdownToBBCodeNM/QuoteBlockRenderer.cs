using Markdig.Syntax;

namespace Converter.MarkdownToBBCodeNM;

public class QuoteBlockRenderer : NexusModsObjectRenderer<QuoteBlock>
{
    protected override void Write(NexusModsRenderer renderer, QuoteBlock obj)
    {
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();
        renderer.WriteLine("[quote]");
        renderer.WriteChildren(obj);
        renderer.EnsureLine();
        renderer.Write("[/quote]");
        if (!renderer.IsLastInContainer) renderer.EnsureLine();
    }
}