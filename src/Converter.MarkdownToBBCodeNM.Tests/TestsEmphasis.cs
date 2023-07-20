using Markdig;

namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsEmphasis
{
    [Test]
    public void Converts_Emphasis_Bold()
    {
        var markdown = $"""
**Hello World!**

__Hello World!__
""";
        var bbCode = """
[b]Hello World![/b]
[b]Hello World![/b]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_Emphasis_Italic()
    {
        var markdown = $"""
*Hello World!*

_Hello World!_
""";
        var bbCode = """
[i]Hello World![/i]
[i]Hello World![/i]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_Emphasis_Strikethrough()
    {
        var markdown = """
~~Hello World!~~
""";
        var bbCode = """
[s]Hello World![/s]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
    [Test]
    public void Converts_Emphasis_BoldItalic()
    {
        var markdown = """
***Hello World!***
""";
        var bbCode = """
[i][b]Hello World![/b][/i]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}