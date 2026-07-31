using Converter.MarkdownToBBCodeSteam;

namespace Converter.MarkdownToBBCode.Tests.Steam;

public class TestsCodeBlock
{
    [Test]
    public void Converts_CodeBlock()
    {
        const string markdown = """
```xml
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
```
""";
        const string bbCode = """
[code]
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
[/code]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_CodeBlock_Generic()
    {
        const string markdown = """
```
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
```
""";
        const string bbCode = """
[code]
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
[/code]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_CodeBlock_Inline()
    {
        const string markdown = """
`<ItemGroup><PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" /></ItemGroup>`
""";
        const string bbCode = """
[b][noparse]<ItemGroup><PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" /></ItemGroup>[/noparse][/b]
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_CodeBlock_Inline_BBCodeContent()
    {
        // [noparse] keeps literal BBCode inside a code span from being interpreted by Steam
        const string markdown = """
Use `[spoiler]` to hide content
""";
        const string bbCode = """
Use [b][noparse][spoiler][/noparse][/b] to hide content
""";
        Assert.That(MarkdownSteam.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}