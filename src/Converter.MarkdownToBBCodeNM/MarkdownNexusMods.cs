using Converter.MarkdownToBBCode.Shared;

namespace Converter.MarkdownToBBCodeNM;

public static class MarkdownNexusMods
{
    public static string ToBBCode(string markdown) => BBCodeConverter.Convert(markdown, BBCodeType.NexusMods, extended: false);

    public static string ToBBCodeExtended(string markdown) => BBCodeConverter.Convert(markdown, BBCodeType.NexusMods, extended: true);
}