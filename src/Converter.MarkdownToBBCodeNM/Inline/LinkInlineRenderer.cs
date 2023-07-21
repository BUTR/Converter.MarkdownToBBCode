using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCodeNM.Inline;

public class LinkInlineRenderer : NexusModsObjectRenderer<LinkInline>
{
    protected override void Write(NexusModsRenderer renderer, LinkInline link)
    {
        var url = link.GetDynamicUrl != null ? link.GetDynamicUrl() ?? link.Url : link.Url;

        if (link.IsImage)
        {
            renderer.Write($"[img]{url}[/img]");
        }
        else
        {
            const string youtube = "https://www.youtube.com/watch?v=";
            if (url.StartsWith(youtube))
            {
                renderer.Write($"[youtube]");
                renderer.Write(url.Substring(youtube.Length));
                renderer.Write("[/youtube]");
                return;
            }

            renderer.Write($"[url={url}]");
            renderer.WriteChildren(link);
            renderer.Write("[/url]");
        }
    }
}