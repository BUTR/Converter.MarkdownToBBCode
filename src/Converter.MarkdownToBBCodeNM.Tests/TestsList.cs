namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsList
{
    [Test]
    public void Converts_ListOrdered()
    {
        var markdown = """
1. One
2. Two
3. Three
""";
        var bbCode = """
[list=1]
[*]One
[*]Two
[*]Three
[/list]

""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_ListUnordered()
    {
        var markdown = """
* One
* Two
* Three
""";
        var bbCode = """
[list]
[*]One
[*]Two
[*]Three
[/list]

""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_ListNested()
    {
        var markdown = $"""
* One
  * One One
  * One Two
* Two
  1. Two One
  2. Two Two
* Three

1. One
  * One One
  * One Two
2. Two
  1. Two One
  2. Two Two
3. Three

""";
        var bbCode = """
[list]
[*]One
[list]
[*]One One
[*]One Two
[/list]
[*]Two
[list=1]
[*]Two One
[*]Two Two
[/list]
[*]Three
[/list]
[list=1]
[*]One
[/list]
[list]
[*]One One
[*]One Two
[/list]
[list=1]
[*]Two
[*]Two One
[*]Two Two
[*]Three
[/list]

""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}