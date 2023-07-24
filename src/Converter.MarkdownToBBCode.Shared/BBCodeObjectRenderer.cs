using Markdig.Renderers;
using Markdig.Syntax;

namespace Converter.MarkdownToBBCode.Shared;

public abstract class BBCodeObjectRenderer<TObject> : MarkdownObjectRenderer<BBCodeRenderer, TObject> where TObject : MarkdownObject { }