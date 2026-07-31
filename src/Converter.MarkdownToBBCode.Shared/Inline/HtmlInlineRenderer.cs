using Converter.MarkdownToBBCode.Shared.Html;

using HtmlAgilityPack;

using Markdig.Syntax.Inlines;

namespace Converter.MarkdownToBBCode.Shared.Inline;

// Handles HTML inlines nested within other inlines (e.g. **<a href="URL">TEXT</a>**),
// which are not covered by HtmlUtils.ProcessLeafBlock
public class HtmlInlineRenderer : BBCodeObjectRenderer<HtmlInline>
{
    protected override void Write(BBCodeRenderer renderer, HtmlInline obj)
    {
        var tag = obj.Tag;
        if (string.IsNullOrEmpty(tag)) return;

        if (!renderer.HandleHTML)
        {
            renderer.Write(tag);
            return;
        }

        if (tag.StartsWith("</"))
        {
            switch (tag.Trim('<', '>', '/', ' ').ToLowerInvariant())
            {
                case "b" or "strong":
                    renderer.Write("[/b]");
                    return;
                case "i" or "em":
                    renderer.Write("[/i]");
                    return;
                case "ins" or "u":
                    renderer.Write("[/u]");
                    return;
                case "s" or "strike":
                    renderer.Write("[/s]");
                    return;
                case "a":
                    renderer.Write("[/url]");
                    return;
                default:
                    return;
            }
        }

        var document = new HtmlDocument();
        document.LoadHtml(tag);
        if (document.DocumentNode.FirstChild is not { } node) return;

        switch (node.Name)
        {
            case "br":
                renderer.EnsureLine();
                return;
            case "b" or "strong":
                renderer.Write("[b]");
                return;
            case "i" or "em":
                renderer.Write("[i]");
                return;
            case "ins" or "u":
                renderer.Write("[u]");
                return;
            case "s" or "strike":
                renderer.Write("[s]");
                return;
            case "a" when node.Attributes["nexusmods_href"] is { Value: { } href } && renderer.BBCodeType == BBCodeType.NexusMods:
                renderer.Write($"[url={href}]");
                return;
            case "a" when node.Attributes["steam_href"] is { Value: { } href } && renderer.BBCodeType == BBCodeType.Steam:
                renderer.Write($"[url={href}]");
                return;
            case "a" when node.Attributes["href"] is { Value: { } href }:
                renderer.Write($"[url={href}]");
                return;
            case "img" when node.Attributes["nexusmods_src"] is { Value: { } src } && renderer.BBCodeType == BBCodeType.NexusMods:
                renderer.Write($"[img{HtmlUtils.GetImgParams(node, renderer.BBCodeType)}]{src}[/img]");
                return;
            case "img" when node.Attributes["steam_src"] is { Value: { } src } && renderer.BBCodeType == BBCodeType.Steam:
                renderer.Write($"[img]{src}[/img]");
                return;
            case "img" when node.Attributes["src"] is { Value: { } src }:
                renderer.Write($"[img{HtmlUtils.GetImgParams(node, renderer.BBCodeType)}]{src}[/img]");
                return;
            default:
                return;
        }
    }
}