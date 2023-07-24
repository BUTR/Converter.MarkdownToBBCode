using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

public class TestsQuote
{
    [Test]
    public void Converts_Quote()
    {
        const string markdown = """
> Hello
> World!
""";
        const string bbCode = """
[quote]
Hello
World!
[/quote]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_Quote_Multiple()
    {
        const string markdown = $"""
> Hello
> World!

> Bye
> World!
""";
        const string bbCode = """
[quote]
Hello
World!
[/quote]
[quote]
Bye
World!
[/quote]
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
}