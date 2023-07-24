using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

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
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
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
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_LinkYoutube()
    {
        const string markdown = """
[https://www.youtube.com/watch?v=ID](https://www.youtube.com/watch?v=ID)
""";
        const string bbCode = """
https://www.youtube.com/watch?v=ID
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_LinkSteamstore()
    {
        const string markdown = """
[https://store.steampowered.com/app/323190/](https://store.steampowered.com/app/323190/)
""";
        const string bbCode = """
https://store.steampowered.com/app/323190/
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_LinkSteamWorkshop()
    {
        const string markdown = """
[https://steamcommunity.com/sharedfiles/filedetails/?id=157328145](https://steamcommunity.com/sharedfiles/filedetails/?id=157328145)
""";
        const string bbCode = """
https://steamcommunity.com/sharedfiles/filedetails/?id=157328145
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}