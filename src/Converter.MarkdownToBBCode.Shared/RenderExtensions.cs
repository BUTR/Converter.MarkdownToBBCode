using Converter.MarkdownToBBCode.Shared.Html;

using Markdig.Helpers;
using Markdig.Syntax;

using System.Linq;

namespace Converter.MarkdownToBBCode.Shared;

internal static class RenderExtensions
{
    public static void WriteLinesBefore(this BBCodeRenderer renderer, Block block)
    {
        foreach (var slice in block.LinesBefore ?? Enumerable.Empty<StringSlice>())
        {
            if (slice.NewLine != NewLine.None) renderer.WriteLine();
        }
    }
    public static void WriteLinesAfter(this BBCodeRenderer renderer, Block block)
    {
        foreach (var slice in block.LinesAfter ?? Enumerable.Empty<StringSlice>())
        {
            if (slice.NewLine != NewLine.None) renderer.WriteLine();
        }
    }

    public static void WriteLinesStart(this BBCodeRenderer renderer, Block block)
    {
        var idx = block.Parent?.IndexOf(block);
        var nextElement = idx is null or <= 0 ? null : block.Parent?[idx.Value - 1];
        var isHtml = nextElement is HtmlBlock;
        var isRenderedHtml = nextElement is HtmlBlock htmlBlock && HtmlUtils.CanProcess(renderer, htmlBlock.Lines.ToSlice().AsSpan().ToString());

        if ((!isHtml || isRenderedHtml)) renderer.WriteLinesBefore(block);
        if ((!isHtml || isRenderedHtml) && !renderer.IsFirstInContainer) renderer.EnsureLine();
    }

    public static void WriteLinesEnd(this BBCodeRenderer renderer, Block block)
    {
        var idx = block.Parent?.IndexOf(block);
        var nextBlock = idx is null || block.Parent?.Count <= idx + 1 ? null : block.Parent?[idx.Value + 1];

        var isHtml = block is HtmlBlock;
        var isRenderedHtml = block is HtmlBlock htmlBlock && HtmlUtils.CanProcess(renderer, htmlBlock.Lines.ToSlice().AsSpan().ToString());
        var isNextBlockHtml = nextBlock is HtmlBlock;
        var isNextBlockRenderedHtml = nextBlock is HtmlBlock nextHtmlBlock && HtmlUtils.CanProcess(renderer, nextHtmlBlock.Lines.ToSlice().AsSpan().ToString());


        if ((!isNextBlockHtml || isNextBlockRenderedHtml) && (!renderer.IsLastInContainer || block.NewLine != NewLine.None)) renderer.EnsureLine();
        if ((!isNextBlockHtml || isNextBlockRenderedHtml) && renderer.IsLastInContainer) renderer.WriteLinesAfter(block);
    }
}