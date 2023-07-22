# Converter.MarkdownToBBCodeNM
Converts Markdown into NexusMods BBCode. Might be opionated with the HTML conversion, since there are a few ways to interpret HTML

### Installation
```shell
dotnet tool install -g Converter.MarkdownToBBCodeNM.Tool
```

### Usage
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

## Example
At the time of writing the tool, it was used to keep in sync the description of the BLSE mod for Bannerlord.  
BLSE - [GitHub](https://github.com/BUTR/Bannerlord.BLSE) -> [NexusMods](https://www.nexusmods.com/mountandblade2bannerlord/mods/1)

## Notes
* You can ignore an HTML element by adding `converter_ignore` attribute to the element
  ```HTML
  <p converter_ignore>WILL NOT BE CONVERTED TO BBCODE</p>
  ```

## Supporting Codes
| BBCode                                 | Markdown                                                                  | Implementation |
|----------------------------------------|---------------------------------------------------------------------------|----------------|
| \[b\]TEXT\[\b]                         | \*\*TEXT\*\*                                                              | Markdown       |
| \[i\]TEXT\[/i]                         | \*TEXT\*                                                                  | Markdown       |
| \[u]TEXT\[/u]                          | \<ins\>TEXT\<\/ins\> OR \<u\>TEXT\<\/u\>                                  | HTML           |
| \[s]TEXT\[/s]                          | \~\~TEXT\~\~                                                              | Markdown       |
| \[url=URL]TEXT\[/url]                  | \[TEXT\]\(URL\)                                                           | Markdown       |
| \[img]URL\[/img]                       | \!\[Alt text\]\(URL\)                                                     | Markdown       |
| \[quote]TEXT\[/quote]                  | \> TEXT                                                                   | Markdown       |
| \[quote AUTHOR]TEXT\[/quote]           | \> TEXT                                                                   | Markdown       |
| \[code]CODE\[/code]                    | \`\`\`CODE\`\`\`                                                          | Markdown       |
| \[list=1]\[*]ENTRY\[/list]             | 1. ENTRY                                                                  | Markdown       |
| \[list]\[*]ENTRY\[/list]               | \* ENTRY                                                                  | Markdown       |
| \[line]                                | \<hr\/\>                                                                  | HTML           |
| \[color=COLOR]TEXT\[/color]            |                                                                           |                |
| \[font=FONT]TEXT\[/font]               |                                                                           |                |
| \[center]TEXT\[/center]                | \<p align=\"center\"\>TEXT\<\/p\>                                         | HTML           |
| \[right]TEXT\[/right]                  | \<p align=\"right\"\>TEXT\<\/p\>                                          | HTML           |
| \[left]TEXT\[/left]                    | \<p align=\"left\"\>TEXT\<\/p\>                                           | HTML           |
| \[size=1]TEXT\[/size]                  | ###### TEXT                                                               | Markdown       |
| \[size=2]TEXT\[/size]                  | ##### TEXT                                                                | Markdown       |
| \[size=3]TEXT\[/size]                  | #### TEXT                                                                 | Markdown       |
| \[size=4]TEXT\[/size]                  | ### TEXT                                                                  | Markdown       |
| \[size=5]TEXT\[/size]                  | ## TEXT                                                                   | Markdown       |
| \[size=6]TEXT\[/size]                  | # TEXT                                                                    | Markdown       |
| \[spoiler]SUMMARY\\n\\rTEXT\[/spoiler] | \<details\>\<summary\>SUMMARY\<\/summary\>TEXT\<\/details\>               | HTML           |
| \[youtube]ID\[/youtube]                | \[https://www.youtube.com/watch?v=ID](https://www.youtube.com/watch?v=ID) | Markdown       |
