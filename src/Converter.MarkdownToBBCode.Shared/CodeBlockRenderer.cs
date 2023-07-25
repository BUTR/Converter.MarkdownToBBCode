using Markdig.Helpers;
using Markdig.Syntax;

using System;

namespace Converter.MarkdownToBBCode.Shared;

public class CodeBlockRenderer : BBCodeObjectRenderer<CodeBlock>
{
    protected override void Write(BBCodeRenderer renderer, CodeBlock obj)
    {
        renderer.WriteLinesStart(obj);

        // NexusMods can't render when the code block conains the language
        //renderer.WriteLine(obj is FencedCodeBlock { Info: { } info } && !string.IsNullOrEmpty(info) ? $"[code={info}]" : "[code]");
        renderer.Write("[code]");
        renderer.EnsureLine();
        WriteLeafRawLines(renderer, obj, true);
        renderer.EnsureLine();
        renderer.Write("[/code]");

        renderer.WriteLinesEnd(obj);
    }

    private void WriteLeafRawLines(BBCodeRenderer renderer, LeafBlock? leafBlock, bool writeEndOfLines)
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