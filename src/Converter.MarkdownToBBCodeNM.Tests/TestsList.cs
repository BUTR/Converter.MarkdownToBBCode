namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsList
{
    [Test]
    public void Converts_ListOrdered()
    {
        const string markdown = """
1. One
2. Two
3. Three
""";
        const string bbCode = """
[list=1]
[*]One
[*]Two
[*]Three[/list]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_ListUnordered()
    {
        const string markdown = """
* One
* Two
* Three
""";
        const string bbCode = """
[list]
[*]One
[*]Two
[*]Three[/list]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_ListNested()
    {
        const string markdown = $"""
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
        const string bbCode = """
[list]
[*]One
[list]
[*]One One
[*]One Two[/list]
[*]Two
[list=1]
[*]Two One
[*]Two Two[/list]
[*]Three[/list]
[list=1]
[*]One[/list]
[list]
[*]One One
[*]One Two[/list]
[list=1]
[*]Two
[*]Two One
[*]Two Two
[*]Three[/list]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}