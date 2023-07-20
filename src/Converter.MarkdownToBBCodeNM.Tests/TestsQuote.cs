namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsQuote
{
    [Test]
    public void Converts_Quote()
    {
        var markdown = """
> Hello
> World!
""";
        var bbCode = """
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
        var markdown = $"""
> Hello
> World!

> Bye
> World!
""";
        var bbCode = """
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