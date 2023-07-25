using Markdig.Helpers;
using Markdig.Syntax;

using System;

namespace Converter.MarkdownToBBCode.Shared;

public class ListRenderer : BBCodeObjectRenderer<ListBlock>
{
    protected override void Write(BBCodeRenderer renderer, ListBlock obj)
    {
        renderer.WriteLinesStart(obj);

        switch (renderer.BBCodeType)
        {
            case BBCodeType.NexusMods:
                renderer.Write(obj.IsOrdered ? "[list=1]" : "[list]");
                for (var i = 0; i < obj.Count; i++)
                {
                    var listItem = (ListItemBlock) obj[i];
                    renderer.EnsureLine();
                    renderer.Write("[*]");
                    renderer.WriteChildren(listItem);
                    // Don't add newline to the last item because it breaks NexusMods nested list rendering
                    if (i != obj.Count - 1) renderer.EnsureLine();
                }
                renderer.Write("[/list]");
                break;
            case BBCodeType.Steam:
                renderer.Write(obj.IsOrdered ? "[olist]" : "[list]");
                for (var i = 0; i < obj.Count; i++)
                {
                    var listItem = (ListItemBlock) obj[i];
                    renderer.EnsureLine();
                    renderer.Write("[*]");
                    renderer.WriteChildren(listItem);
                    renderer.EnsureLine();
                }
                renderer.Write(obj.IsOrdered ? "[/olist]" : "[/list]");
                break;
        }

        renderer.WriteLinesEnd(obj);
    }
}