using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

public class TestsThematicBreak
{
    [Test]
    public void Converts_ThematicBreak()
    {
        const string markdown = """
sfsdf

---

sdf
""";
        const string bbCode = """
sfsdf

[hr][/hr]

sdf
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_ThematicBreak_Asterisks()
    {
        const string markdown = """
***
""";
        const string bbCode = """
[hr][/hr]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}