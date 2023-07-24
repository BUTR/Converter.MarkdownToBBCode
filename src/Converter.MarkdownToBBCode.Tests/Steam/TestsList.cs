using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

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
[olist]
[*] One
[*] Two
[*] Three
[/olist]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
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
[*] One
[*] Two
[*] Three
[/list]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
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
[*] One
[list]
[*] One One
[*] One Two
[/list]
[*] Two
[olist]
[*] Two One
[*] Two Two
[/olist]
[*] Three
[/list]
[olist]
[*] One
[/olist]
[list]
[*] One One
[*] One Two
[/list]
[olist]
[*] Two
[*] Two One
[*] Two Two
[*] Three
[/olist]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}