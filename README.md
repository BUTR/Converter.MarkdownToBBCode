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
# -d or --disableextended will disable newline detection via two spaces
# and will disable HTML conversion
markdown_to_bbcodenm -i "~~raw\r\nmarkdown~~" --disableextended

markdown_to_bbcodenm -i "/markdown.md";
markdown_to_bbcodenm -i "/markdown.md" -o "/bbcode.txt";
```

## Usage
markdown_to_bbcodenm -i "**rawcode**";
markdown_to_bbcodenm -i "/markdown.md";