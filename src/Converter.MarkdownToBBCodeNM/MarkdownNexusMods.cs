using Markdig;
using Markdig.Extensions.EmphasisExtras;
using Markdig.Parsers;

using System.IO;

namespace Converter.MarkdownToBBCodeNM;

// TODO: No artibrary line breaks
public static class MarkdownNexusMods
{
    // I'm a genius
    internal static string ToBBCodeReuse(string markdown, bool? doubleLineBreakAsNewLine, NexusModsRenderer rendererOld)
    {
        var document = MarkdownParser.Parse(markdown, rendererOld.Pipeline);

        using var writer = new StringWriter();
        var renderer = new NexusModsRenderer(rendererOld.Pipeline, doubleLineBreakAsNewLine ?? rendererOld.DoubleLineBreakAsNewLine, rendererOld.HandleHTML, writer);
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }

    public static string ToBBCode(string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder().UseEmphasisExtras(EmphasisExtraOptions.Strikethrough).Build();

        var document = MarkdownParser.Parse(markdown, pipeline);

        using var writer = new StringWriter();
        var renderer = new NexusModsRenderer(pipeline, false, false, writer);
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }

    public static string ToBBCodeExtended(string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder().UseEmphasisExtras(EmphasisExtraOptions.Strikethrough).Build();

        var document = MarkdownParser.Parse(markdown, pipeline);

        using var writer = new StringWriter();
        var renderer = new NexusModsRenderer(pipeline, true, true, writer);
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }
}