using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

public class TestsMarkers
{
    [Test]
    public void Keeps_Steam_Region_Drops_NexusMods_And_Ignore()
    {
        const string markdown = """
Intro.

<!-- converter_steam -->
Steam only line.
<!-- /converter_steam -->
<!-- converter_nexusmods -->
Nexus only line.
<!-- /converter_nexusmods -->
<!-- converter_ignore -->
Hidden line.
<!-- /converter_ignore -->

Outro.
""";
        const string bbCode = """
Intro.

Steam only line.

Outro.
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Blank_Line_Between_Fences_Does_Not_Leak()
    {
        // The removed NexusMods region leaves a blank line between its fence and the Steam fence;
        // it must not surface as a gap between the heading and the first Steam section
        const string markdown = """
## FAQ
<!-- converter_nexusmods -->
Nexus content.
<!-- /converter_nexusmods -->

<!-- converter_steam -->
### Steam Section
Steam content.
<!-- /converter_steam -->
""";
        const string bbCode = """
[h2]FAQ[/h2]
[h3]Steam Section[/h3]
Steam content.

""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}