﻿using Converter.MarkdownToBBCodeNM;

using Markdig;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

public class TestsEmphasis
{
    [Test]
    public void Converts_Emphasis_Bold()
    {
        const string markdown = """
**Hello World!**

__Hello World!__
""";
        const string bbCode = """
[b]Hello World![/b]

[b]Hello World![/b]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_Emphasis_Italic()
    {
        const string markdown = """
*Hello World!*

_Hello World!_
""";
        const string bbCode = """
[i]Hello World![/i]

[i]Hello World![/i]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_Emphasis_Strikethrough()
    {
        const string markdown = """
~~Hello World!~~
""";
        const string bbCode = """
[s]Hello World![/s]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
    [Test]
    public void Converts_Emphasis_BoldItalic()
    {
        const string markdown = """
***Hello World!***
""";
        const string bbCode = """
[i][b]Hello World![/b][/i]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_Emphasis_Spaces()
    {
        const string markdown = """
This is a **Hello World** !
""";
        const string bbCode = """
This is a [b]Hello World[/b] !
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}