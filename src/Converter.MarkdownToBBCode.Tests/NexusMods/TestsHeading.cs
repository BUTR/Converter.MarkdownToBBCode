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
}