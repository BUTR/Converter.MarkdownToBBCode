using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

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
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}