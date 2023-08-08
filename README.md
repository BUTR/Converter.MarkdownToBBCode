# Converter.MarkdownToBBCode
Converts Markdown and HTML (GitHub flavor) into NexusMods and Steam BBCode flavor. Might be opionated with the HTML conversion, since there are a few ways to interpret HTML

### Installation
```shell
dotnet tool install -g Converter.MarkdownToBBCodeNM.Tool
dotnet tool install -g Converter.MarkdownToBBCodeSteam.Tool
```

### Usage
When installed as a global tool:
```shell
# NexusMods
markdown_to_bbcodenm -i "**raw markdown**"
markdown_to_bbcodenm -i "~~raw\r\nmarkdown~~" --disableextended

markdown_to_bbcodenm -i "/markdown.md";
markdown_to_bbcodenm -i "/markdown.md" -o "/bbcode.txt";

# Steam
markdown_to_bbcodesteam -i "**raw markdown**"
markdown_to_bbcodesteam -i "~~raw\r\nmarkdown~~" --disableextended

markdown_to_bbcodesteam -i "/markdown.md";
markdown_to_bbcodesteam -i "/markdown.md" -o "/bbcode.txt";
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
* You can ignore an HTML element by adding `converter_ignore`, `converter_nexusmods`, `converter_steam` attribute to the element
  ```HTML
  <p converter_ignore>WILL NOT BE CONVERTED TO BBCODE</p>
  <p converter_nexusmods>WILL NOT BE CONVERTED TO BBCODE FOR STEAM</p>
  <p converter_steam>WILL NOT BE CONVERTED TO BBCODE FOR NEXUSMODS</p>
  ```
* You can set an alternative `href` for a link for NexusMods/Steam by adding a `nexusmods_href` or `steam_href` attribute to the element
  ```HTML
  <a href="MARKDOWN_LINK" nexusmods_href="NEXUSMODS_LINK" /> </a>
  ```
* You can set an alternative `src` for an image for NexusMods/Steam by adding a `nexusmods_src` or `steam_src` attribute to the element
  ```HTML
  <img src="MARKDOWN_IMAGE" nexusmods_src="NEXUSMODS_IMAGE" />
  ```
## Supporting Codes
[GitHub Markdown Supported HTML Codes](https://github.com/gjtorikian/html-pipeline/blob/a2e02ac8372da5376cde623466dfaeb0f2b2ea1c/lib/html_pipeline/sanitization_filter.rb)  
[Steam Supported BBCode Codes](https://steamcommunity.com/comment/ForumTopic/formattinghelp)  

| NexusMods BBCode                       | Steam BBCode                       | Markdown (GitHub)                                                         | HTML                                                                   |
|----------------------------------------|------------------------------------|---------------------------------------------------------------------------|------------------------------------------------------------------------|
| \[b\]TEXT\[\b]                         | \[b\]TEXT\[\b]                     | \*\*TEXT\*\*                                                              | \<b>TEXT\</b>                                                          |
| \[i\]TEXT\[/i]                         | \[i\]TEXT\[/i]                     | \*TEXT\*                                                                  | \<i>TEXT\</i>                                                          |
| \[u]TEXT\[/u]                          | \[u]TEXT\[/u]                      |                                                                           | \<ins\>TEXT\</ins> OR \<u\>TEXT\</u>                                   |
| \[s]TEXT\[/s]                          | \[strike]TEXT\[/strike]            | \~\~TEXT\~\~                                                              | \<s>TEXT\</s> OR \<strike>TEXT\</strike>                               |
| \[url=URL]TEXT\[/url]                  | \[url=URL]TEXT\[/url]              | \[TEXT\]\(URL\)                                                           | \<a href="URL"\>TEXT\</a>                                              |
| \[img]URL\[/img]                       | \[img]URL\[/img]                   | \!\[Alt text\]\(URL\)                                                     | \<img src="URL">\</img>                                                |
| \[quote]TEXT\[/quote]                  | \[quote]TEXT\[/quote]              | \> TEXT                                                                   | \<blockquote>TEXT\</blockquote>                                        |
| \[quote AUTHOR]TEXT\[/quote]           | \[quote=AUTHOR]TEXT\[/quote]       | \> TEXT                                                                   |                                                                        |
| \[code]CODE\[/code]                    | \[code]CODE\[/code]                | \`\`\`CODE\`\`\`                                                          | \<code>CODE\</code>                                                    |
| \[list=1]\[*]ENTRY\[/list]             | \[olist]\[*]ENTRY\[/olist]         | 1. ENTRY                                                                  | \<ol>\<li>ENTRY\</li>\</ol>                                            |
| \[list]\[*]ENTRY\[/list]               | \[list]\[*]ENTRY\[/list]           | \* ENTRY                                                                  | \<ul>\<li>ENTRY\</li>\</ul>                                            |
| \[line]                                | \[hr]\[/hr]                        |                                                                           | \<hr/>                                                                 |
| \[color=COLOR]TEXT\[/color]            |                                    |                                                                           |                                                                        |
| \[font=FONT]TEXT\[/font]               |                                    |                                                                           |                                                                        |
| \[center]TEXT\[/center]                |                                    |                                                                           | \<p align=\"center\"\>TEXT\</p> OR \<div align=\"center\"\>TEXT\</div> |
| \[right]TEXT\[/right]                  |                                    |                                                                           | \<p align=\"right\"\>TEXT\</p> OR \<div align=\"right\"\>TEXT\</div>   |
| \[left]TEXT\[/left]                    |                                    |                                                                           | \<p align=\"left\"\>TEXT\</p> OR \<div align=\"left\"\>TEXT\</div>     |
| \[size=1]TEXT\[/size]                  | \[h6]TEXT\[/h6]                    | ###### TEXT                                                               | \<h6\>TEXT\</h6>                                                       |
| \[size=2]TEXT\[/size]                  | \[h5]TEXT\[/h5]                    | ##### TEXT                                                                | \<h5\>TEXT\</h5>                                                       |
| \[size=3]TEXT\[/size]                  | \[h4]TEXT\[/h4]                    | #### TEXT                                                                 | \<h4\>TEXT\</h4>                                                       |
| \[size=4]TEXT\[/size]                  | \[h3]TEXT\[/h3]                    | ### TEXT                                                                  | \<h3\>TEXT\</h3>                                                       |
| \[size=5]TEXT\[/size]                  | \[h2]TEXT\[/h2]                    | ## TEXT                                                                   | \<h2\>TEXT\</h2>                                                       |
| \[size=6]TEXT\[/size]                  | \[h1]TEXT\[/h1]                    | # TEXT                                                                    | \<h1\>TEXT\</h1>                                                       |
| \[spoiler]SUMMARY\\n\\rTEXT\[/spoiler] | INLINE SPOILERS NOT MAPPED TO HTML |                                                                           | \<details>\<summary>SUMMARY\</summary>TEXT\</details>                  |
| \[youtube]ID\[/youtube]                | https://www.youtube.com/watch?v=ID | \[https://www.youtube.com/watch?v=ID](https://www.youtube.com/watch?v=ID) |                                                                        |
