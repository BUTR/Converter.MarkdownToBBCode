using CommandLine;

using System;
using System.IO;

namespace Converter.MarkdownToBBCodeSteam.Tool;

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
                    var bbcode = o.DisableExtendedFeatures ? MarkdownSteam.ToBBCode(content) : MarkdownSteam.ToBBCodeExtended(content);
                    if (!string.IsNullOrEmpty(o.OutputFilePath))
                        File.WriteAllText(o.OutputFilePath, bbcode);
                    else
                        Console.Write(bbcode);
                }
                else
                {
                    var bbcode = o.DisableExtendedFeatures ? MarkdownSteam.ToBBCode(o.Input) : MarkdownSteam.ToBBCodeExtended(o.Input);
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