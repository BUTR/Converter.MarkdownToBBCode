using CommandLine;

using System;
using System.IO;

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
    public static int Main(string[] args)
    {
        return Parser.Default
            .ParseArguments<ConvertOptions>(args)
            .MapResult(Convert, _ =>
            {
                Console.Write("INVALID COMMAND");
                return 1;
            });
    }

    private static int Convert(ConvertOptions o)
    {
        var content = File.Exists(o.Input) ? File.ReadAllText(o.Input) : o.Input;
        var bbcode = o.DisableExtendedFeatures ? MarkdownNexusMods.ToBBCode(content) : MarkdownNexusMods.ToBBCodeExtended(content);

        if (!string.IsNullOrEmpty(o.OutputFilePath))
            File.WriteAllText(o.OutputFilePath, bbcode);
        else
            Console.Write(bbcode);

        return 0;
    }
}
