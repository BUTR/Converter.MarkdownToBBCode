namespace Converter.MarkdownToBBCode.Shared;

public enum BBCodeType
{
    NexusMods,
    Steam
}

public static class BBCodeTypeExtensions
{
    // Steam renders strikethrough as [strike], NexusMods as [s]
    public static string StrikeTag(this BBCodeType type) => type == BBCodeType.Steam ? "strike" : "s";
}