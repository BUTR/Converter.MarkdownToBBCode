using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

public class TestsHTML
{
    [Test]
    public void Converts_HTML_InlineEntity()
    {
        const string markdown = """
fdfsdf <b>This is a &auml; blank</b> sf
""";
        const string bbCode = """
fdfsdf [b]This is a ä blank[/b] sf
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
    [Test]
    public void Converts_HTML_Inline()
    {
        const string markdown = """
foo <hr/> bar
""";
        const string bbCode = """
foo [hr][/hr] bar
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
[b]Details[/b]
Something small enough to escape casual notice.
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
[b]Details[/b]
Something small enough to escape casual notice.
Something small enough to escape casual notice.
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
[b]Xbox Game Pass PC[/b]
You need to copy content of '/bin/Gaming.Desktop.x64_Shipping_Client' from BLSE to 'Mount & Blade II- Bannerlord/Content/bin/Gaming.Desktop.x64_Shipping_Client'
[img]https://media.discordapp.net/attachments/422092475163869201/1088721252702765126/image.png[/img]
You need to copy content of 'Modules/Bannerlord.Harmony' from Harmony to 'Mount & Blade II- Bannerlord/Content/Modules/Bannerlord.Harmony'
[img]https://media.discordapp.net/attachments/422092475163869201/1088721253692616775/image.png[/img]

""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
[h1]Bannerlord.BLSE[/h1]
[url=https://github.com/BUTR/Bannerlord.UIExtenderEx]
[img]https://media.discordapp.net/attachments/422092475163869201/1083742477250465843/BLSE_SMALL_SMALL.png[/img]
[/url]

""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_Nested2() // This path triggers Paragrap parsing
    {
        const string markdown = """
# Bannerlord.BLSE
<a href="https://github.com/BUTR/Bannerlord.UIExtenderEx" ><img alt="Logo" src="https://media.discordapp.net/attachments/422092475163869201/1083742477250465843/BLSE_SMALL_SMALL.png" /></a>
""";
        const string bbCode = """
[h1]Bannerlord.BLSE[/h1]
[url=https://github.com/BUTR/Bannerlord.UIExtenderEx][img]https://media.discordapp.net/attachments/422092475163869201/1083742477250465843/BLSE_SMALL_SMALL.png[/img][/url]
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
[hr][/hr]
sdf
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Quote()
    {
        const string markdown = """
<blockquote>sfsdf</blockquote>
""";
        const string bbCode = """
[quote]sfsdf[/quote]
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Code()
    {
        const string markdown = """
<code>sfsdf</code>
""";
        const string bbCode = """
[code]sfsdf[/code]
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_List()
    {
        const string markdown = """
<ol>
  <li>sfsdf</li>
</ol>

<ul>
  <li>sfsdf</li>
</ul>
""";
        const string bbCode = """
[olist]
[*]sfsdf
[/olist]
[list]
[*]sfsdf
[/list]
""";
        Assert.That(MarkdownSteam.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Heading()
    {
        const string markdown = """
<h1>One</h1>
<h2>Two</h2>
<h3>Three</h3>
<h4>Four</h4>
<h5>Five</h5>
<h6>Six</h6>
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


    [Test]
    public void Converts_HTML_Spacing()
    {
        const string markdown = """
<p>
  Hello!
  <br>
</p>

<p converter_steam text="For the Early Access version please visit [this!](https://steamcommunity.com/sharedfiles/filedetails/?id=2880546785)"/>


AKA MBOptionScreen Standalone.
""";
        const string bbCode = """
Hello!

For the Early Access version please visit [url=https://steamcommunity.com/sharedfiles/filedetails/?id=2880546785]this![/url]

AKA MBOptionScreen Standalone.
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Etc()
    {
        const string markdown = """
## FAQ
<p converter_steam>
  <p text="### CTD After an Update:" />
  <br>
  <p text="Steam is not always updating all mods. We suggest to manually resubscribe to Harmony, UIExtenderEx, ButterLib and MCM if you experience crashes!" />
  <br>
  <p text="### Wrong MCM Version:" />
  <br>
  <p text="Check that the Modules folder (C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules) does not contain a Bannerlord.MBOptionScreen folder!" />
</p>

<p converter_nexusmods>
  <h3>How do I install it?</h3>
  <ol>
    <li>Find the Modules folder in your Bannerlord installation location. For Steam users, this is usually here: <code>C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules</code></li>
    <li>Download the latest version of this mod and drop the folder inside Modules from the archive into your game's <code>Modules</code> folder.</li>
    <li>Start the Mount & Blade II: Bannerlord launcher and select all Mod Configuration Menu mods to be loaded.</li>
  </ol>
  <details>
    <summary>Your Modules folder structure should look something like this:</summary>
    <img src="https://cdn.discordapp.com/attachments/753640646253740073/858635739528429568/unknown.png">
  </details>
  <details>
    <summary>Your mod order in the launcher should look something like this:</summary>
    <img src="https://cdn.discordapp.com/attachments/753640646253740073/858636433450729492/unknown.png">
  </details>
  <br>
  <h3>Unblocking DLL's</h3>
  <p>You may need to right click on every \*.dll files, click Properties, and click Unblock if you extracted the zip file with Windows Explorer or other programs that try to secure extracted files.</p>
  <br>
  <h3>Crashes on v1.0.0-v1.0.3 with Vortex used</h3>
  <p>Check <a href="https://forum.nexusmods.com/index.php?showtopic=8605808/#entry118785353">this comment</a></p>
</p>

### Harmony installation issues:
Check Harmony's Troubleshooting for more info on how to fix it.
""";
        const string bbCode = """
[h2]FAQ[/h2]
[h3]CTD After an Update:[/h3]
Steam is not always updating all mods. We suggest to manually resubscribe to Harmony, UIExtenderEx, ButterLib and MCM if you experience crashes!
[h3]Wrong MCM Version:[/h3]
Check that the Modules folder (C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules) does not contain a Bannerlord.MBOptionScreen folder!
[h3]Harmony installation issues:[/h3]
Check Harmony's Troubleshooting for more info on how to fix it.
""";
        var t = MarkdownSteam.ToBBCode(markdown);
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}