using Converter.MarkdownToBBCodeNM.Inline;

using Markdig;
using Markdig.Renderers;

using System.IO;

namespace Converter.MarkdownToBBCodeNM;

public class NexusModsRenderer : TextRendererBase<NexusModsRenderer>
{
    internal MarkdownPipeline Pipeline { get; set; }
    internal bool HTMLForceNewLine { get; set; }

    public bool DoubleLineBreakAsNewLine { get; set; }
    public bool HandleHTML { get; set; }


    public NexusModsRenderer(MarkdownPipeline pipeline, bool doubleLineBreakAsNewLine, bool handleHTML, TextWriter writer) : base(writer)
    {
        Pipeline = pipeline;
        DoubleLineBreakAsNewLine = doubleLineBreakAsNewLine;
        HandleHTML = handleHTML;

        ObjectRenderers.Add(new CodeBlockRenderer());
        ObjectRenderers.Add(new ListRenderer());
        ObjectRenderers.Add(new HeadingRenderer());
        if (handleHTML) ObjectRenderers.Add(new HtmlBlockRenderer());
        ObjectRenderers.Add(new ParagraphRenderer());
        ObjectRenderers.Add(new QuoteBlockRenderer());
        //ObjectRenderers.Add(new ThematicBreakRenderer());

        // Default inline renderers
        //ObjectRenderers.Add(new AutolinkInlineRenderer());
        ObjectRenderers.Add(new CodeInlineRenderer());
        //ObjectRenderers.Add(new DelimiterInlineRenderer());
        ObjectRenderers.Add(new EmphasisInlineRenderer());
        ObjectRenderers.Add(new LineBreakInlineRenderer());
        //if (handleHTML) ObjectRenderers.Add(new HtmlInlineRenderer());
        //if (handleHTML) ObjectRenderers.Add(new HtmlEntityInlineRenderer());
        ObjectRenderers.Add(new LinkInlineRenderer());
        ObjectRenderers.Add(new LiteralInlineRenderer());
    }
}