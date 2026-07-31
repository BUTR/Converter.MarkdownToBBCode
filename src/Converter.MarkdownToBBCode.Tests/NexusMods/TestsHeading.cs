using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

public class TestsHeading
{
    [Test]
    public void Converts_Heading()
    {
        const string markdown = """
# One
## Two
### Three
#### Four

##### Five


###### Six
""";
        const string bbCode = """
[size=6]One[/size]
[size=5]Two[/size]
[size=4]Three[/size]
[size=3]Four[/size]

[size=2]Five[/size]


[size=1]Six[/size]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_Heading_NoBlankLineBeforeFollowingText()
    {
        // A blank line after a heading (lint-required) must not become a rendered gap
        const string markdown = """
### Crashes with Vortex

Check this comment.
""";
        const string bbCode = """
[size=4]Crashes with Vortex[/size]
Check this comment.
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}