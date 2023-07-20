namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsHTML
{
    [Test]
    public void Converts_HTML_Align()
    {
        var markdown = """
<p align="right">
dfgdfg
</p>
""";
        var bbCode = """
[right]
dfgdfg
[/right]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_AlignInline()
    {
        var markdown = """
<p align="right">dfgdfg</p>
""";
        var bbCode = """
[right]dfgdfg[/right]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_AlignNested()
    {
        var markdown = """
<p align="right">
**Hello World!**
</p>
""";
        var bbCode = """
[right]
[b]Hello World![/b]
[/right]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Underscore()
    {
        var markdown = """
<u>
dfgdfg
</u>

<ins>
dfgdfg
</ins>
""";
        var bbCode = """
[u]
dfgdfg
[/u]
[u]
dfgdfg
[/u]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_UnderscoreInline()
    {
        var markdown = """
<u>dfgdfg</u>
<u><div>dfgdfg</div></u>

<ins>dfgdfg</ins>
""";
        var bbCode = """
[u]dfgdfg[/u]
[u]dfgdfg[/u]
[u]dfgdfg[/u]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_UnderscoreNested()
    {
        var markdown = """
<u>
**Hello World!**
</u>

<ins>
**Hello World!**
</ins>
""";
        var bbCode = """
[u]
[b]Hello World![/b]
[/u]
[u]
[b]Hello World![/b]
[/u]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Spoiler()
    {
        var markdown = """
<details>
    <summary>Details</summary>
    Something small enough to escape casual notice.
</details>
""";
        var bbCode = """
[spoiler]
Something small enough to escape casual notice.
[/spoiler]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_SpoilerMultiline()
    {
        var markdown = """
<details>
    <summary>Details</summary>
    Something small enough to escape casual notice.
    Something small enough to escape casual notice.
</details>
""";
        var bbCode = """
[spoiler]
Something small enough to escape casual notice.
Something small enough to escape casual notice.
[/spoiler]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
}