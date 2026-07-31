using Converter.MarkdownToBBCode.Shared;

namespace Converter.MarkdownToBBCodeSteam;

public static class MarkdownSteam
{
    public static string ToBBCode(string markdown) => BBCodeConverter.Convert(markdown, BBCodeType.Steam, extended: false);

    public static string ToBBCodeExtended(string markdown) => BBCodeConverter.Convert(markdown, BBCodeType.Steam, extended: true);
}
