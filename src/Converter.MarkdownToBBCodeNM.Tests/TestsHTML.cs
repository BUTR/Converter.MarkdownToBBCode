namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsHTML
{
    [Test]
    public void Converts_HTML_Align()
    {
        const string markdown = """
<p align="right">
dfgdfg
</p>
""";
        const string bbCode = """
[right]
dfgdfg
[/right]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_AlignInline()
    {
        const string markdown = """
<p align="right">dfgdfg</p>
""";
        const string bbCode = """
[right]dfgdfg[/right]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_AlignNested()
    {
        const string markdown = """
<p align="right">
**Hello World!**
</p>
""";
        const string bbCode = """
[right]
[b]Hello World![/b]
[/right]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Underscore()
    {
        const string markdown = """
<u>
dfgdfg
</u>

<ins>
dfgdfg
</ins>
""";
        const string bbCode = """
[u]
dfgdfg
[/u]
[u]
dfgdfg
[/u]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_UnderscoreInline()
    {
        const string markdown = """
<u>dfgdfg</u>

<u><div>dfgdfg</div></u>

<ins>dfgdfg</ins>
""";
        const string bbCode = """
[u]dfgdfg[/u]

[u]dfgdfg[/u]

[u]dfgdfg[/u]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_UnderscoreNested()
    {
        const string markdown = """
<u>
**Hello World!**
</u>

<ins>
**Hello World!**
</ins>
""";
        const string bbCode = """
[u]
[b]Hello World![/b]
[/u]
[u]
[b]Hello World![/b]
[/u]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Spoiler()
    {
        const string markdown = """
<details>
    <summary>Details</summary>
    Something small enough to escape casual notice.
</details>
""";
        const string bbCode = """
[spoiler]
[b]Details[/b]
Something small enough to escape casual notice.
[/spoiler]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_SpoilerMultiline()
    {
        const string markdown = """
<details>
    <summary>Details</summary>
    Something small enough to escape casual notice.
    Something small enough to escape casual notice.
</details>
""";
        const string bbCode = """
[spoiler]
[b]Details[/b]
Something small enough to escape casual notice.
Something small enough to escape casual notice.
[/spoiler]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_SpoilerNested()
    {
        const string markdown = """
<details>
  <summary>Xbox Game Pass PC</summary>
  <p>You need to copy content of '/bin/Gaming.Desktop.x64_Shipping_Client' from BLSE to 'Mount & Blade II- Bannerlord/Content/bin/Gaming.Desktop.x64_Shipping_Client'</p>
  <img src="https://media.discordapp.net/attachments/422092475163869201/1088721252702765126/image.png" alt="BLSE Installation Path" width="500">
  <p>You need to copy content of 'Modules/Bannerlord.Harmony' from Harmony to 'Mount & Blade II- Bannerlord/Content/Modules/Bannerlord.Harmony'</p>
  <img src="https://media.discordapp.net/attachments/422092475163869201/1088721253692616775/image.png" alt="Bannerlord.Harmony Installation Path" width="500">
</details>
""";
        const string bbCode = """
[spoiler]
[b]Xbox Game Pass PC[/b]
You need to copy content of '/bin/Gaming.Desktop.x64_Shipping_Client' from BLSE to 'Mount & Blade II- Bannerlord/Content/bin/Gaming.Desktop.x64_Shipping_Client'
[img]https://media.discordapp.net/attachments/422092475163869201/1088721252702765126/image.png[/img]
You need to copy content of 'Modules/Bannerlord.Harmony' from Harmony to 'Mount & Blade II- Bannerlord/Content/Modules/Bannerlord.Harmony'
[img]https://media.discordapp.net/attachments/422092475163869201/1088721253692616775/image.png[/img]

[/spoiler]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Nested()
    {
        const string markdown = """
# Bannerlord.BLSE
<p align="center">
  <a href="https://github.com/BUTR/Bannerlord.UIExtenderEx" alt="Logo">
    <img src="https://media.discordapp.net/attachments/422092475163869201/1083742477250465843/BLSE_SMALL_SMALL.png" />
  </a>
  </br>
</p>
""";
        const string bbCode = """
[size=6]Bannerlord.BLSE[/size]
[center]
[url=https://github.com/BUTR/Bannerlord.UIExtenderEx]
[img]https://media.discordapp.net/attachments/422092475163869201/1083742477250465843/BLSE_SMALL_SMALL.png[/img]
[/url]

[/center]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_Nested2() // This path triggers Paragrap parsing
    {
        const string markdown = """
# Bannerlord.BLSE
<a href="https://github.com/BUTR/Bannerlord.UIExtenderEx" ><img alt="Logo" src="https://media.discordapp.net/attachments/422092475163869201/1083742477250465843/BLSE_SMALL_SMALL.png" /></a>
""";
        const string bbCode = """
[size=6]Bannerlord.BLSE[/size]
[url=https://github.com/BUTR/Bannerlord.UIExtenderEx][img]https://media.discordapp.net/attachments/422092475163869201/1083742477250465843/BLSE_SMALL_SMALL.png[/img][/url]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Line()
    {
        const string markdown = """
sfsdf
<hr/>
sdf
""";
        const string bbCode = """
sfsdf
[line]
sdf
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
}