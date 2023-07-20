using Markdig.Syntax;
using Markdig.Syntax.Inlines;

using System;

namespace Converter.MarkdownToBBCodeNM;

public class ParagraphRenderer : NexusModsObjectRenderer<ParagraphBlock>
{
    protected override void Write(NexusModsRenderer renderer, ParagraphBlock obj)
    {
        if (!renderer.IsFirstInContainer) renderer.EnsureLine();

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

        renderer.WriteLeafInline(obj);

        if (!renderer.IsLastInContainer) renderer.EnsureLine();
    }
}