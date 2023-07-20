using Markdig.Syntax;

using System;

namespace Converter.MarkdownToBBCodeNM;

public class CodeBlockRenderer : NexusModsObjectRenderer<CodeBlock>
{
    protected override void Write(NexusModsRenderer renderer, CodeBlock obj)
    {
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();
        renderer.WriteLine(obj is FencedCodeBlock { Info: { } info } && !string.IsNullOrEmpty(info) ? $"[code={info}]" : "[code]");
        WriteLeafRawLines(renderer, obj, true);
        renderer.Write("[/code]");
        if (!renderer.IsLastInContainer) renderer.EnsureLine();
    }

    private void WriteLeafRawLines(NexusModsRenderer renderer, LeafBlock? leafBlock, bool writeEndOfLines)
    {
        if (leafBlock is null) throw new ArgumentNullException(nameof(leafBlock));

        var slices = leafBlock.Lines.Lines;
        if (slices is null) return;

        for (var i = 0; i < slices.Length; i++)
        {
            ref var slice = ref slices[i].Slice;
            if (slice.Text is null)
            {
                break;
            }

            if (!writeEndOfLines && i > 0)
            {
                renderer.WriteLine();
            }

            renderer.Write(slice.AsSpan());

            if (writeEndOfLines)
            {
                renderer.WriteLine();
            }
        }
    }
}