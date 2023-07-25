using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

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
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }
    [Test]
    public void Converts_HTML_Inline()
    {
        const string markdown = """
foo <hr/> bar
""";
        const string bbCode = """
foo [line] bar
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
    }


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
    public void Converts_HTML_Strike()
    {
        const string markdown = """
<s>dfgdfg</s>

<s><div>dfgdfg</div></s>

<strike>dfgdfg</strike>
""";
        const string bbCode = """
[s]dfgdfg[/s]

[s]dfgdfg[/s]

[s]dfgdfg[/s]
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


    [Test]
    public void Converts_HTML_Quote()
    {
        const string markdown = """
<blockquote>sfsdf</blockquote>
""";
        const string bbCode = """
[quote]sfsdf[/quote]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
[list=1]
[*]sfsdf
[/list]
[list]
[*]sfsdf
[/list]
""";
        Assert.That(MarkdownNexusMods.ToBBCodeExtended(markdown), Is.EqualTo(bbCode));
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
    public void Converts_HTML_SpanConditional()
    {
        const string markdown = """
<img src="google.com" />

<p converter_nexusmods text="For the Early Access version please visit [this!](https://steamcommunity.com/sharedfiles/filedetails/?id=2880546785)"/>
""";
        const string bbCode = """
[img]google.com[/img]
For the Early Access version please visit [url=https://steamcommunity.com/sharedfiles/filedetails/?id=2880546785]this![/url]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Ignore()
    {
        const string markdown = """
# Header
<p converter_steam>
  <p steam_text="### CTD After an Update:" />
  <br>
  <p steam_text="Steam is not always updating all mods. We suggest to manually resubscribe to Harmony, UIExtenderEx, ButterLib and MCM if you experience crashes!" />
  <br>
  <p steam_text="### Wrong MCM Version:" />
  <br>
  <p steam_text="Check that the Modules folder (C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules) does not contain a Bannerlord.MBOptionScreen folder!" />
</p>
""";
        const string bbCode = """
[size=6]Header[/size]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }


    [Test]
    public void Converts_HTML_Headers()
    {
        const string markdown = """
# Header
<h1>test1</h1>
<h1>test2</h1>

<h1>test3</h1>


<h1>test4</h1>
<h1>test5</h1>
""";
        const string bbCode = """
[size=6]Header[/size]
[size=6]test1[/size]
[size=6]test2[/size]
[size=6]test3[/size]

[size=6]test4[/size]
[size=6]test5[/size]
""";
        var t = MarkdownNexusMods.ToBBCode(markdown);
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_Headers2()
    {
        const string markdown = """
## FAQ
<p converter_steam>
  <p text="### CTD After an Update:" />
</p>

### How do I install it?
<p converter_steam>
  <p text="### CTD After an Update:" />
</p>

<p>
  <p text="### CTD After an Update:" />
</p>
""";
        const string bbCode = """
[size=5]FAQ[/size]
[size=4]How do I install it?[/size]
[size=4]CTD After an Update:[/size]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_Headers3()
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
[size=5]FAQ[/size]
[size=4]How do I install it?[/size]
[list=1]
[*]Find the Modules folder in your Bannerlord installation location. For Steam users, this is usually here: [code]C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules[/code][*]Download the latest version of this mod and drop the folder inside Modules from the archive into your game's [code]Modules[/code] folder.[*]Start the Mount & Blade II: Bannerlord launcher and select all Mod Configuration Menu mods to be loaded.[/list]
[spoiler]
[b]Your Modules folder structure should look something like this:[/b]
[img]https://cdn.discordapp.com/attachments/753640646253740073/858635739528429568/unknown.png[/img]
[/spoiler]
[spoiler]
[b]Your mod order in the launcher should look something like this:[/b]
[img]https://cdn.discordapp.com/attachments/753640646253740073/858636433450729492/unknown.png[/img]
[/spoiler]
[size=4]Unblocking DLL's[/size]
You may need to right click on every *.dll files, click Properties, and click Unblock if you extracted the zip file with Windows Explorer or other programs that try to secure extracted files.
[size=4]Crashes on v1.0.0-v1.0.3 with Vortex used[/size]
Check [url=https://forum.nexusmods.com/index.php?showtopic=8605808/#entry118785353]this comment[/url]
[size=4]Harmony installation issues:[/size]
Check Harmony's Troubleshooting for more info on how to fix it.
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_HTML_Spacing()
    {
        const string markdown = """
<p>
  Hello!
</p>

<p converter_steam text="For the Early Access version please visit [this!](https://steamcommunity.com/sharedfiles/filedetails/?id=2880546785)"/>


AKA MBOptionScreen Standalone.
""";
        const string bbCode = """
Hello!

AKA MBOptionScreen Standalone.
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}