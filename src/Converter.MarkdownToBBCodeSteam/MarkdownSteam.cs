using Converter.MarkdownToBBCode.Shared;

using Markdig;
using Markdig.Extensions.EmphasisExtras;
using Markdig.Parsers;

using System.IO;

namespace Converter.MarkdownToBBCodeSteam;

public static class MarkdownSteam
{
    public static string ToBBCode(string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder().EnableTrackTrivia().UseEmphasisExtras(EmphasisExtraOptions.Strikethrough).Build();

        var document = MarkdownParser.Parse(markdown, pipeline);

        using var writer = new StringWriter();
        var renderer = new BBCodeRenderer(BBCodeType.Steam, pipeline, false, false, writer);
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }

    public static string ToBBCodeExtended(string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder().EnableTrackTrivia().UseEmphasisExtras(EmphasisExtraOptions.Strikethrough).Build();

        var document = MarkdownParser.Parse(markdown, pipeline);

        using var writer = new StringWriter();
        var renderer = new BBCodeRenderer(BBCodeType.Steam, pipeline, true, true, writer);
        renderer.Render(document);
        renderer.Writer.Flush();

        return (renderer.Writer.ToString() ?? string.Empty).ReplaceLineEndings();
    }
}