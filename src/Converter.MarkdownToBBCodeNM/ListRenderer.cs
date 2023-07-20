using Markdig.Syntax;

namespace Converter.MarkdownToBBCodeNM;

public class ListRenderer : NexusModsObjectRenderer<ListBlock>
{
    protected override void Write(NexusModsRenderer renderer, ListBlock listBlock)
    {
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();

        renderer.Write(listBlock.IsOrdered ? "[list=1]" : "[list]");
        foreach (ListItemBlock listItem in listBlock)
        {
            renderer.EnsureLine();
            renderer.Write("[*]");
            renderer.WriteChildren(listItem);
            renderer.EnsureLine();
        }
        renderer.WriteLine("[/list]");

        if (!renderer.IsLastInContainer) renderer.EnsureLine();
    }
}