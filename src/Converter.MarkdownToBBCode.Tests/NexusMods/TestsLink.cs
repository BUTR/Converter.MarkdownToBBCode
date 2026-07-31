using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

public class TestsLink
{
    [Test]
    public void Converts_Link()
    {
        const string markdown = """
[This is Google](https://google.com)
""";
        const string bbCode = """
[url=https://google.com]This is Google[/url]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_LinkImage()
    {
        const string markdown = """
![This is Google](https://google.com)
""";
        const string bbCode = """
[img]https://google.com[/img]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_LinkYoutube()
    {
        const string markdown = """
[https://www.youtube.com/watch?v=ID](https://www.youtube.com/watch?v=ID)
""";
        const string bbCode = """
[youtube]ID[/youtube]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_LinkYoutube_ExtraQueryParams()
    {
        const string markdown = """
[video](https://www.youtube.com/watch?v=ID&t=30s)
""";
        const string bbCode = """
[youtube]ID[/youtube]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_LinkYoutube_ShortUrl()
    {
        const string markdown = """
[video](https://youtu.be/ID?t=30)
""";
        const string bbCode = """
[youtube]ID[/youtube]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}