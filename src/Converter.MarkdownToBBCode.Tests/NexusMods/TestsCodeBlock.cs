using Converter.MarkdownToBBCodeNM;

namespace Converter.MarkdownToBBCode.Tests.NexusMods;

public class TestsCodeBlock
{
    [Test]
    public void Converts_CodeBlock()
    {
        const string markdown = """
```
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
```
```xml
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
```

```xml
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
```

`<ItemGroup><PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" /></ItemGroup>`
""";
        const string bbCode = """
[code]
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
[/code]
[code]
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
[/code]

[code]
  <ItemGroup>
    <PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" />
  </ItemGroup>
[/code]

[b]<ItemGroup><PackageReference Include="Bannerlord.MCM" Version="5.9.1" IncludeAssets="compile" /></ItemGroup>[/b]
""";
        Assert.That(MarkdownNexusMods.ToBBCode(markdown), Is.EqualTo(bbCode));
    }
}