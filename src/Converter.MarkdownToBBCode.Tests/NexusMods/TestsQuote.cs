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

> Middle


> Bye
> World!
""";
        const string bbCode = """
[quote]
Hello
World!
[/quote]
[quote]
Middle
[/quote]

[quote]
Bye
World!
[/quote]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}