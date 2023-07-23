using Markdig.Syntax.Inlines;

using System;

namespace Converter.MarkdownToBBCodeNM.Inline;

public class LiteralInlineRenderer : NexusModsObjectRenderer<LiteralInline>
{
    protected override void Write(NexusModsRenderer renderer, LiteralInline obj)
    {
        var span = obj.Content.AsSpan();
        // Ends with one space and there's a next sibling, preserve the space
        if (span.EndsWith(" ") && (span.Length <= 1 || span[^2] != ' ') && obj.NextSibling is not null)
            renderer.Write(span);
        // Starts with a space and there's other symbold
        else if (span.StartsWith(" ") && (span.Length <= 1 || span[1] != ' ') && span.Trim().Length > 0)
            renderer.Write(span);
        // In all other cases, trim whitespaces since we enabled TriviaTracking
        else
            renderer.Write(span.Trim());
    }
}