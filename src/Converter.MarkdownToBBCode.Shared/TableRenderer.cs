using Markdig.Extensions.Tables;

using System.Linq;

namespace Converter.MarkdownToBBCode.Shared;

public class TableRenderer : BBCodeObjectRenderer<Table>
{
    protected override void Write(BBCodeRenderer renderer, Table table)
    {
        renderer.WriteLinesStart(table);

        switch (renderer.BBCodeType)
        {
            case BBCodeType.Steam:
                renderer.WriteLine("[table]");
                foreach (var row in table.OfType<TableRow>())
                {
                    var cellTag = row.IsHeader ? "th" : "td";
                    renderer.Write("[tr]");
                    foreach (var cell in row.OfType<TableCell>())
                    {
                        renderer.Write($"[{cellTag}]");
                        renderer.WriteChildren(cell);
                        renderer.Write($"[/{cellTag}]");
                    }
                    renderer.WriteLine("[/tr]");
                }
                renderer.Write("[/table]");
                break;

            // NexusMods BBCode has no table tags, emit the rows as plain lines
            case BBCodeType.NexusMods:
                foreach (var (_, row, _, isLastRow) in table.OfType<TableRow>().WithMetadata())
                {
                    if (row.IsHeader) renderer.Write("[b]");
                    foreach (var (_, cell, isFirstCell, _) in row.OfType<TableCell>().WithMetadata())
                    {
                        if (!isFirstCell) renderer.Write(" | ");
                        renderer.WriteChildren(cell);
                    }
                    if (row.IsHeader) renderer.Write("[/b]");
                    if (!isLastRow) renderer.WriteLine();
                }
                break;
        }

        renderer.WriteLinesEnd(table);
    }
}