using Markdig.Renderers;
using Markdig.Syntax;

namespace Converter.MarkdownToBBCodeNM;

public abstract class NexusModsObjectRenderer<TObject> : MarkdownObjectRenderer<NexusModsRenderer, TObject> where TObject : MarkdownObject { }