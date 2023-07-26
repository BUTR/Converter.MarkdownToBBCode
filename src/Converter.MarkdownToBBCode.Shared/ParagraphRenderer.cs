using Converter.MarkdownToBBCode.Shared.Html;

using Markdig.Helpers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

using System;

namespace Converter.MarkdownToBBCode.Shared;

public class ParagraphRenderer : BBCodeObjectRenderer<ParagraphBlock>
{
    private void ProcessDoubleLineBreak(BBCodeRenderer renderer, ParagraphBlock obj)
    {
        // Some Markdown flavor has the ability to split the lines without adding a line break
        if (renderer.DoubleLineBreakAsNewLine && obj.Parent is not QuoteBlock && obj.Inline is not null)
        {
            foreach (var inline in obj.Inline)
            {
                if (inline is LineBreakInline { PreviousSibling: LiteralInline literalInline })
                {
                    var end = literalInline.Content.End;
                    var endSpace1 = literalInline.Content.Text.Length > end + 1 && literalInline.Content.Text[end + 1] == ' ';
                    var endSpace2 = literalInline.Content.Text.Length > end + 2 && literalInline.Content.Text[end + 2] == ' ';

                    var span = literalInline.Content.Text.AsSpan(literalInline.Content.Start, endSpace1 && endSpace2 ? literalInline.Content.Length + 2 : literalInline.Content.Length);
                    var hasWhitespaceNewLine = span.Length > 1 && span[^1] == ' ' && span[^2] == ' ';

                    if (!hasWhitespaceNewLine) inline.ReplaceBy(new LiteralInline(" "));
                }
            }
        }
    }

    protected override void Write(BBCodeRenderer renderer, ParagraphBlock obj)
    {
        // Not sure if I'm right here
        if (obj.Parent is MarkdownDocument) renderer.WriteLinesBefore(obj);
        if (obj.Parent is MarkdownDocument && !renderer.IsFirstInContainer) renderer.EnsureLine();

        ProcessDoubleLineBreak(renderer, obj);

        HtmlUtils.ProcessLeafBlock(renderer, obj);

        // Write everything that is left
        renderer.WriteLeafInline(obj);

        // Not sure if I'm right here
        if (obj.Parent is MarkdownDocument && !renderer.IsLastInContainer) renderer.EnsureLine();
        if (obj.Parent is MarkdownDocument && obj.NewLine != NewLine.None) renderer.EnsureLine();
        if (obj.Parent is MarkdownDocument or ListItemBlock && !renderer.IsNested) renderer.WriteLinesAfter(obj);
    }
}