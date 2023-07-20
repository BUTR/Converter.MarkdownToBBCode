namespace Converter.MarkdownToBBCodeNM.Tests;

public class TestsCodeBlock
{
    [Test]
    public void Converts_CodeBlock()
    {
        var markdown = """
```xml
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
```
""";
        var bbCode = """
[code=xml]
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
[/code]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_CodeBlock_Generic()
    {
        var markdown = """
```
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
```
""";
        var bbCode = """
[code]
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
[/code]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_CodeBlock_Inline()
    {
        var markdown = """
`<ItemGroup><PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" /></ItemGroup>`
""";
        var bbCode = """
[code]<ItemGroup><PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" /></ItemGroup>[/code]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}