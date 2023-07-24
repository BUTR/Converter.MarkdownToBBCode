using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

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
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
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
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
}