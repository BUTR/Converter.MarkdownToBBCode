using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

public class TestsMarkers
{
    [Test]
    public void Keeps_NexusMods_Region_Drops_Steam_And_Ignore()
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

Nexus only line.

Outro.
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}