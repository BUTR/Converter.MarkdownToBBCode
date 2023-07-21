# Converter.MarkdownToBBCodeNM
Converts Markdown into NexusMods BBCode

### Installation
```shell
dotnet tool install -g Converter.MarkdownToBBCodeNM.Tool
```

### Example
When installed as a global tool:
```shell
markdown_to_bbcodenm -i "**raw markdown**"
markdown_to_bbcodenm -i "~~raw\r\nmarkdown~~" --disableextended

markdown_to_bbcodenm -i "/markdown.md";
markdown_to_bbcodenm -i "/markdown.md" -o "/bbcode.txt";
```
`-i or --input` accepts both raw markdown and a file path.  
`-o or --output` accepts a file path. If specified, will write the
converted BBCode to the file instead of outputting to the console.  
`-d or --disableextended` will disable newline detection via two spaces
and will disable HTML conversion

## Usage
markdown_to_bbcodenm -i "**rawcode**";
markdown_to_bbcodenm -i "/markdown.md";
