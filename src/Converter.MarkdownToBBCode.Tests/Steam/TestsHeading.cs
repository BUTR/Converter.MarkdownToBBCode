using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

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
[h1]One[/h1]
[h2]Two[/h2]
[h3]Three[/h3]
[h4]Four[/h4]
[h5]Five[/h5]
[h6]Six[/h6]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}