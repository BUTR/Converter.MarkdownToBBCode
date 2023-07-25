using Converter.MarkdownToBBCode.Shared.Html;
using Converter.MarkdownToBBCode.Shared.Inline;

using Markdig;
using Markdig.Renderers;

using System.IO;

namespace Converter.MarkdownToBBCode.Shared;

public class BBCodeRenderer : TextRendererBase<BBCodeRenderer>
{
    internal MarkdownPipeline Pipeline { get; }
    internal bool IsNested { get; init; }
    internal bool HTMLForceNewLine { get; init; }

    public bool DoubleLineBreakAsNewLine { get; set; }
    public bool HandleHTML { get; set; }

    public BBCodeType BBCodeType { get; private set; }

    public BBCodeRenderer(BBCodeType type, MarkdownPipeline pipeline, bool doubleLineBreakAsNewLine, bool handleHTML, TextWriter writer) : base(writer)
    {
        Pipeline = pipeline;

        DoubleLineBreakAsNewLine = doubleLineBreakAsNewLine;
        HandleHTML = handleHTML;

        BBCodeType = type;

        ObjectRenderers.Add(new CodeBlockRenderer());
        ObjectRenderers.Add(new ListRenderer());
        ObjectRenderers.Add(new HeadingRenderer());
        ObjectRenderers.Add(new HtmlBlockRenderer());
        ObjectRenderers.Add(new ParagraphRenderer());
        ObjectRenderers.Add(new QuoteBlockRenderer());

        // Default inline renderers
        ObjectRenderers.Add(new CodeInlineRenderer());
        ObjectRenderers.Add(new EmphasisInlineRenderer());
        ObjectRenderers.Add(new LineBreakInlineRenderer());
        //ObjectRenderers.Add(new HtmlInlineRenderer()); // Will never be processed because of ParagraphRenderer
        //ObjectRenderers.Add(new HtmlEntityInlineRenderer());  // Will never be processed because of ParagraphRenderer
        ObjectRenderers.Add(new LinkInlineRenderer());
        ObjectRenderers.Add(new LiteralInlineRenderer());
    }
}