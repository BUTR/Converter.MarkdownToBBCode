using Markdig;
using Markdig.Extensions.EmphasisExtras;
using Markdig.Parsers;

using System.IO;

namespace Converter.MarkdownToBBCode.Shared;

public static class BBCodeConverter
{
    public static string Convert(string markdown, BBCodeType type, bool extended)
    {
        var pipeline = new MarkdownPipelineBuilder().EnableTrackTrivia().UsePipeTables().UseEmphasisExtras(EmphasisExtraOptions.Strikethrough).Build();

        markdown = ConverterMarkers.Apply(markdown, type);
        var document = MarkdownParser.Parse(markdown, pipeline);

        using var writer = new StringWriter();
        var renderer = new BBCodeRenderer(type, pipeline, extended, extended, writer) { Source = markdown };
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }
}
