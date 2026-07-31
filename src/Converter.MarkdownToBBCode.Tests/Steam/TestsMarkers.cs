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
}