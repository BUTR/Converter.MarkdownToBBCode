using Converter.MarkdownToBBCode.Shared;

using Markdig;
using Markdig.Extensions.EmphasisExtras;
using Markdig.Parsers;

using System.IO;

namespace Converter.MarkdownToBBCodeNM;

public static class MarkdownNexusMods
{
    public static string ToBBCode(string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder().EnableTrackTrivia().UsePipeTables().UseEmphasisExtras(EmphasisExtraOptions.Strikethrough).Build();

        var document = MarkdownParser.Parse(markdown, pipeline);

        using var writer = new StringWriter();
        var renderer = new BBCodeRenderer(BBCodeType.NexusMods, pipeline, false, false, writer) { Source = markdown };
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }

    public static string ToBBCodeExtended(string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder().EnableTrackTrivia().UsePipeTables().UseEmphasisExtras(EmphasisExtraOptions.Strikethrough).Build();

        var document = MarkdownParser.Parse(markdown, pipeline);

        using var writer = new StringWriter();
        var renderer = new BBCodeRenderer(BBCodeType.NexusMods, pipeline, true, true, writer) { Source = markdown };
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }
}