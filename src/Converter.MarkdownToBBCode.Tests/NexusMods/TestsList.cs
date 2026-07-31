using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

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
[ol]
[li] One[/li]
[li] Two[/li]
[li] Three[/li]
[/ol]
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
[ul]
[li] One[/li]
[li] Two[/li]
[li] Three[/li]
[/ul]
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
[ul]
[li] One
[ul]
[li] One One[/li]
[li] One Two[/li]
[/ul][/li]
[li] Two
[ol]
[li] Two One[/li]
[li] Two Two[/li]
[/ol][/li]
[li] Three
[/li]
[/ul]
[ol]
[li] One[/li]
[/ol]
[ul]
[li] One One[/li]
[li] One Two[/li]
[/ul]
[ol]
[li] Two[/li]
[li] Two One[/li]
[li] Two Two[/li]
[li] Three[/li]
[/ol]
""";
        var t = MarkdownNexusMods.ToBBCode(markdown);
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}