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
            const string steamstore = "https://store.steampowered.com/";
            const string steamworkshop = "https://steamcommunity.com/sharedfiles/";
            switch (renderer.BBCodeType)
            {
                case BBCodeType.NexusMods when url.StartsWith(youtube):
                    renderer.Write("[youtube]");
                    renderer.Write(url.Substring(youtube.Length));
                    renderer.Write("[/youtube]");
                    return;
                /* Looks like it's not working
                case BBCodeType.Steam when url.StartsWith(youtube) || url.StartsWith(steamstore) || url.StartsWith(steamworkshop):
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