using Markdig.Syntax.Inlines;

using System;

namespace Converter.MarkdownToBBCode.Shared.Inline;

public class LinkInlineRenderer : BBCodeObjectRenderer<LinkInline>
{
    private static readonly string[] YouTubePrefixes =
    {
        "https://www.youtube.com/watch?v=",
        "https://youtube.com/watch?v=",
        "https://youtu.be/",
    };

    // [youtube] takes the bare video id, so extra query params (&t=30s) must be cut off
    private static bool TryGetYouTubeId(string? url, out string id)
    {
        id = string.Empty;
        if (url is null) return false;

        foreach (var prefix in YouTubePrefixes)
        {
            if (!url.StartsWith(prefix)) continue;
            var rest = url.Substring(prefix.Length);
            var end = rest.IndexOfAny(new[] { '&', '?', '#' });
            id = end >= 0 ? rest.Substring(0, end) : rest;
            return id.Length > 0;
        }
        return false;
    }

    protected override void Write(BBCodeRenderer renderer, LinkInline link)
    {
        var url = link.GetDynamicUrl != null ? link.GetDynamicUrl() ?? link.Url : link.Url;

        if (link.IsImage)
        {
            renderer.Write($"[img]{url}[/img]");
        }
        else
        {
            switch (renderer.BBCodeType)
            {
                case BBCodeType.NexusMods when TryGetYouTubeId(url, out var videoId):
                    renderer.Write("[youtube]");
                    renderer.Write(videoId);
                    renderer.Write("[/youtube]");
                    return;
                /* Looks like it's not working
                case BBCodeType.Steam when url.StartsWith("https://www.youtube.com/watch?v=") || url.StartsWith("https://store.steampowered.com/") || url.StartsWith("https://steamcommunity.com/sharedfiles/"):
                    renderer.Write(url);
                    return;
                */
                default:
                    renderer.Write($"[url={url}]");
                    renderer.WriteChildren(link);
                    renderer.Write("[/url]");
                    return;
            }
        }
    }
}