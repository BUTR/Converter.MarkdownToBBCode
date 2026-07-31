using System.Text.RegularExpressions;

namespace Converter.MarkdownToBBCode.Shared;

// Platform scoping via HTML-comment fences that GitHub renders as nothing, e.g.
//   <!-- converter_steam -->      ...only emitted for Steam...      <!-- /converter_steam -->
//   <!-- converter_nexusmods -->  ...only emitted for NexusMods... <!-- /converter_nexusmods -->
//   <!-- converter_ignore -->     ...never emitted...              <!-- /converter_ignore -->
// The fenced content stays plain Markdown; only the invisible fences are stripped before parsing.
public static class ConverterMarkers
{
    // Whole region including both fence comments (and the trailing newline of the closing fence)
    private static string RemoveRegion(string markdown, string scope) =>
        Regex.Replace(markdown, $@"[ \t]*<!--\s*{scope}\s*-->.*?<!--\s*/{scope}\s*-->[ \t]*\r?\n?", string.Empty,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

    // Just the fence comments, leaving the Markdown content in place
    private static string Unwrap(string markdown, string scope) =>
        Regex.Replace(markdown, $@"[ \t]*<!--\s*/?{scope}\s*-->[ \t]*\r?\n?", string.Empty, RegexOptions.IgnoreCase);

    public static string Apply(string markdown, BBCodeType type)
    {
        var keep = type == BBCodeType.Steam ? "converter_steam" : "converter_nexusmods";
        var drop = type == BBCodeType.Steam ? "converter_nexusmods" : "converter_steam";

        markdown = RemoveRegion(markdown, drop);
        markdown = RemoveRegion(markdown, "converter_ignore");
        markdown = Unwrap(markdown, keep);
        return markdown;
    }
}