using CommandLine;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.MarkdownToBBCodeNM.Tool;

[Verb("convert", true, HelpText = "Converts the input")]
public class ConvertOptions
{
    [Option('i', "input", Required = true)]
    public string Input { get; set; } = default!;

    [Option('o', "output", Required = false)]
    public string? OutputFilePath { get; set; }

    [Option('d', "disableextended", Required = false)]
    public bool DisableExtendedFeatures { get; set; }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var parser = Parser.Default
            .ParseArguments<ConvertOptions>(args);
        parser = parser
            .WithParsed<ConvertOptions>(o =>
            {
                if (File.Exists(o.Input))
                {
                    var content = File.ReadAllText(o.Input);
                    var bbcode = o.DisableExtendedFeatures ? MarkdownNexusMods.ToBBCode(content) : MarkdownNexusMods.ToBBCodeExtended(content);
                    if (!string.IsNullOrEmpty(o.OutputFilePath))
                        File.WriteAllText(o.OutputFilePath, bbcode);
                    else
                        Console.Write(bbcode);
                }
                else
                {
                    var bbcode = o.DisableExtendedFeatures ? MarkdownNexusMods.ToBBCode(o.Input) : MarkdownNexusMods.ToBBCodeExtended(o.Input);
                    if (!string.IsNullOrEmpty(o.OutputFilePath))
                        File.WriteAllText(o.OutputFilePath, bbcode);
                    else
                        Console.Write(bbcode);
                }

            });
        parser = parser
            .WithNotParsed(e => { Console.Write("INVALID COMMAND"); });
    }
}