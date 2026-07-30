using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

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
[table]
[tr][th]Date[/th][th]Version[/th][th]Notes[/th][/tr]
[tr][td]23/08/2023[/td][td]4.2.0[/td][td]update for new patch[/td][/tr]
[tr][td]02/08/2023[/td][td]4.1.1[/td][td]strengthen null checks[/td][/tr]
[/table]
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
[b]2023[/b]
[table]
[tr][th]Date[/th][th]Version[/th][/tr]
[tr][td]23/08/2023[/td][td]4.2.0[/td][/tr]
[/table]

""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
}
