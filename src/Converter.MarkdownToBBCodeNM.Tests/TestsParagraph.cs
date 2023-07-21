namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsParagraph
{
    private const char Space = (char) 32;

    [Test]
    public void Converts_TestsParagraph()
    {
        const string markdown = $"""
This is not
one line

This is one line
""";
        const string bbCode = """
This is not
one line
This is one line
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_TestsParagraph_LineBreak()
    {
        const string markdown = $"""
This is
one line

This is not{Utils.LineBreak}
one line

This is one line
""";
        const string bbCode = """
This is one line
This is not
one line
This is one line
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
}