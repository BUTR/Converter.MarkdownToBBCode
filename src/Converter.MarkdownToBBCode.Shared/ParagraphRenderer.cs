using Converter.MarkdownToBBCode.Shared.Html;

using Markdig.Helpers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

using System.Linq;

namespace Converter.MarkdownToBBCode.Shared;

public class ParagraphRenderer : BBCodeObjectRenderer<ParagraphBlock>
{
    private void ProcessDoubleLineBreak(BBCodeRenderer renderer, ParagraphBlock obj)
    {
        // Some Markdown flavor has the ability to split the lines without adding a line break
        if (!renderer.DoubleLineBreakAsNewLine || obj.Parent is QuoteBlock || obj.Inline is null) return;

        // Snapshot first: ReplaceBy detaches the node being enumerated, which would stop the loop
        var softBreaks = obj.Inline.OfType<LineBreakInline>().Where(x => !x.IsHard).ToList();
        foreach (var lineBreak in softBreaks)
        {
            lineBreak.ReplaceBy(new LiteralInline(" "));
        }
    }

    private static Block? Sibling(Block block, int offset)
    {
        if (block.Parent is not { } parent) return null;
        var idx = parent.IndexOf(block) + offset;
        return idx >= 0 && idx < parent.Count ? parent[idx] : null;
    }

    protected override void Write(BBCodeRenderer renderer, ParagraphBlock obj)
    {
        // Both platforms render a blank line as a paragraph break; a blank line between a
        // heading/list and its adjacent paragraph adds a spurious gap, so collapse it.
        var suppressBefore = Sibling(obj, -1) is HeadingBlock;
        var suppressAfter = Sibling(obj, +1) is ListBlock;

        // Not sure if I'm right here
        if (obj.Parent is MarkdownDocument && !suppressBefore) renderer.WriteLinesBefore(obj);
        if (obj.Parent is MarkdownDocument && !renderer.IsFirstInContainer) renderer.EnsureLine();

        ProcessDoubleLineBreak(renderer, obj);

        HtmlUtils.ProcessLeafBlock(renderer, obj);

        // Write everything that is left
        renderer.WriteLeafInline(obj);

        // Not sure if I'm right here
        if (obj.Parent is MarkdownDocument && !renderer.IsLastInContainer) renderer.EnsureLine();
        if (obj.Parent is MarkdownDocument && obj.NewLine != NewLine.None) renderer.EnsureLine();
        if (obj.Parent is MarkdownDocument or ListItemBlock && !renderer.IsNested && !suppressAfter) renderer.WriteLinesAfter(obj);
    }
}