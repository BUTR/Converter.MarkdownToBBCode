namespace Converter.MarkdownToBBCodeNM.Tests;

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
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
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
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }

    [Test]
    public void Converts_CodeBlock_Inline()
    {
        const string markdown = """
`<ItemGroup><PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" /></ItemGroup>`
""";
        const string bbCode = """
[b]<ItemGroup><PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" /></ItemGroup>[/b]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}