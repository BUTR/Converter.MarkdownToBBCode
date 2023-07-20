namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsHeading
{
    [Test]
    public void Converts_Heading()
    {
        var markdown = """
# One
## Two
### Three
#### Four
##### Five
###### Six
""";
        var bbCode = """
[size=1]One[/size]
[size=2]Two[/size]
[size=3]Three[/size]
[size=4]Four[/size]
[size=5]Five[/size]
[size=6]Six[/size]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}