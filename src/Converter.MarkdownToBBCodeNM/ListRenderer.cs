using Markdig.Syntax;

namespace Converter.MarkdownToBBCodeNM;

public class ListRenderer : NexusModsObjectRenderer<ListBlock>
{
    protected override void Write(NexusModsRenderer renderer, ListBlock listBlock)
    {
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();

        renderer.Write(listBlock.IsOrdered ? "[list=1]" : "[list]");
        for (var i = 0; i < listBlock.Count; i++)
        {
            var listItem = (ListItemBlock) listBlock[i];
            renderer.EnsureLine();
            renderer.Write("[*]");
            renderer.WriteChildren(listItem);
            // Don't add newline to the last item because it breaks NexusMods nested list rendering
            if (i != listBlock.Count - 1) renderer.EnsureLine();
        }
        renderer.Write("[/list]");

        if (!renderer.IsLastInContainer) renderer.EnsureLine();
    }
}