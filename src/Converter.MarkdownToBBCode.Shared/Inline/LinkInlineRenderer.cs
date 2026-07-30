using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCode.Shared.Inline;

public class LinkInlineRenderer : BBCodeObjectRenderer<LinkInline>
{
    protected override void Write(BBCodeRenderer renderer, LinkInline link)
    {
        var url = link.GetDynamicUrl != null ? link.GetDynamicUrl() ?? link.Url : link.Url;

        if (link.IsImage)
        {
            renderer.Write($"[img]{url}[/img]");
        }
        else
        {
            const string youtube = "https://www.youtube.com/watch?v=";
            switch (renderer.BBCodeType)
            {
                case BBCodeType.NexusMods when url is not null && url.StartsWith(youtube):
                    renderer.Write("[youtube]");
                    renderer.Write(url.Substring(youtube.Length));
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