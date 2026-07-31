using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

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

[line]

sdf
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_ThematicBreak_Asterisks()
    {
        const string markdown = """
***
""";
        const string bbCode = """
[line]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}
