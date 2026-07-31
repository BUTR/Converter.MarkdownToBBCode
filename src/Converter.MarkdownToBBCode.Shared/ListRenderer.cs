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
                renderer.Write(obj.IsOrdered ? "[ol]" : "[ul]");
                for (var i = 0; i < obj.Count; i++)
                {
                    var listItem = (ListItemBlock) obj[i];
                    renderer.EnsureLine();
                    renderer.Write("[li]");
                    renderer.WriteChildren(listItem);
                    renderer.Write("[/li]");
                }
                renderer.EnsureLine();
                renderer.Write(obj.IsOrdered ? "[/ol]" : "[/ul]");
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