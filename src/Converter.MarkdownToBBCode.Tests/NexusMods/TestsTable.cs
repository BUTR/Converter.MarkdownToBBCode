using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

public class TestsTable
{
    [Test]
    public void Converts_Table()
    {
        const string markdown = """
Date | Version | Notes
--- | --- | ---
23/08/2023 | 4.2.0 | update for new patch
02/08/2023 | 4.1.1 | strengthen null checks
""";
        const string bbCode = """
[b]Date | Version | Notes[/b]
23/08/2023 | 4.2.0 | update for new patch
02/08/2023 | 4.1.1 | strengthen null checks
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_Table_InsideDetails()
    {
        const string markdown = """
<details>
<summary>2023</summary>

Date | Version
--- | ---
23/08/2023 | 4.2.0

</details>
""";
        const string bbCode = """
[spoiler]
[b]2023[/b]
[b]Date | Version[/b]
23/08/2023 | 4.2.0
[/spoiler]

""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
}