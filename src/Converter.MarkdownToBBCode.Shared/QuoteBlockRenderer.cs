using Markdig.Helpers;
using Markdig.Syntax;

using System;

namespace Converter.MarkdownToBBCode.Shared;

public class QuoteBlockRenderer : BBCodeObjectRenderer<QuoteBlock>
{
    protected override void Write(BBCodeRenderer renderer, QuoteBlock obj)
    {
        if (obj.LinesBefore?.Count > 0 && obj.LinesBefore?[0].NewLine != NewLine.None) renderer.WriteLine();
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();

        renderer.Write("[quote]");
        renderer.EnsureLine();
        renderer.WriteChildren(obj);
        renderer.EnsureLine();
        renderer.Write("[/quote]");

        if (!renderer.IsLastInContainer) renderer.EnsureLine();
        if (obj.LinesAfter?.Count > 0 && obj.LinesAfter?[0].NewLine != NewLine.None) renderer.WriteLine();
    }
}