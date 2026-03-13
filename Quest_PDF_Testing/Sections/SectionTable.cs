using Quest_PDF_Testing.Helpers;
using Quest_PDF_Testing.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;

namespace Quest_PDF_Testing.Sections
{
    public class SectionTable : IComponent
    {
        public TableItem Table { get; set; }
        public bool IsHalfPageOnly { get; set; } = false;
        public bool ShouldKeepContentOnSamePage { get; set; } = false;
        public float AvailableWidth { get; set; } = PageSizes.A4.Width;

        public SectionTable()
        {
            Table = new TableItem();
        }

        public void Compose(IContainer container)
        {
            container.PreventPageBreakIf(ShouldKeepContentOnSamePage).MaxWidth(AvailableWidth).PaddingBottom(Constants.PADDING_15).Column(column =>
            {
                column.Item().PaddingBottom(Constants.PADDING_2).Text(Table.Title).FontSize(Constants.TITLE_FONT_SIZE_30)
                    .FontColor(Color.FromHex(Constants.RED_COLOR_HEX));

                List<CellItem> currentCellsToShow = [];
                float currentWidth = 0;
                int cellsToSkip = 0;
                bool wasTableSplit = false;

                foreach (CellItem cellItem in Table.CellItems)
                {
                    if (currentWidth + TransformFromMmToPoints(cellItem.WidthMm) >= AvailableWidth)
                    {
                        wasTableSplit = true;
                        var lastCell = currentCellsToShow.Last();
                        bool wasLastItemRemoved = false;
                        if (currentWidth + TransformFromMmToPoints(Constants.CELL_INITIAL_MAX_WIDTH_IN_MM) >= AvailableWidth)
                        {
                            currentCellsToShow.Remove(currentCellsToShow.Last());
                            wasLastItemRemoved = true;
                            currentWidth -= TransformFromMmToPoints(lastCell.WidthMm);
                        }

                        column.Item().Element(c => RenderTableWithGivenCells(c, currentCellsToShow, Table.Rows, wasTableSplit, cellsToSkip));

                        cellsToSkip += currentCellsToShow.Count;
                        currentCellsToShow.Clear();
                        currentWidth = 0;
                        if (wasLastItemRemoved)
                        {
                            currentCellsToShow.Add(lastCell);
                            currentWidth += TransformFromMmToPoints(lastCell.WidthMm);
                        }

                        currentCellsToShow.Add(cellItem);
                        currentWidth += TransformFromMmToPoints(cellItem.WidthMm);
                    }
                    else
                    {
                        currentCellsToShow.Add(cellItem);
                        currentWidth += TransformFromMmToPoints(cellItem.WidthMm);
                    }
                }

                column.Item().Element(c => RenderTableWithGivenCells(c, currentCellsToShow, Table.Rows, wasTableSplit, cellsToSkip));
            });
        }

        private void RenderTableWithGivenCells(IContainer container, IEnumerable<CellItem> cellsToShow, List<RowItem> rows, bool wasTableSplit, int cellsToSkip)
        {
            container.PaddingBottom(Constants.PADDING_15).Table(table =>
            {
                table.ColumnsDefinition(tableColumns =>
                {
                    if (wasTableSplit)
                        tableColumns.ConstantColumn(Constants.CELL_INITIAL_MAX_WIDTH_IN_MM, Unit.Millimetre);
                    foreach (CellItem cellItem in cellsToShow)
                        tableColumns.ConstantColumn(DetermineMaxWidthOfCell(TransformFromMmToPoints(cellItem.WidthMm), wasTableSplit));
                });

                table.Header(tableHeader =>
                {
                    if (wasTableSplit)
                        tableHeader.Cell().BorderBottom(Constants.BORDER_4).BorderColor(Color.FromHex(Constants.RED_COLOR_HEX)).PaddingBottom(Constants.PADDING_1)
                            .Text("");
                    foreach (CellItem cellItem in cellsToShow)
                        tableHeader.Cell().BorderBottom(Constants.BORDER_4).BorderColor(Color.FromHex(Constants.RED_COLOR_HEX)).PaddingBottom(Constants.PADDING_1)
                            .Text(cellItem.HeaderName);
                });

                int rowIndex = 1;
                foreach (RowItem rowItem in rows)
                {
                    if (wasTableSplit)
                        table.Cell().BackgroundTableRows(rowIndex).BorderBottomForLastTableRow(rowIndex == rows.Count).Text($"{rowIndex}");
                    foreach (string item in rowItem.Items.Skip(cellsToSkip).Take(cellsToShow.Count()))
                    {
                        table.Cell().BackgroundTableRows(rowIndex).BorderBottomForLastTableRow(rowIndex == rows.Count).Text(item);
                    }
                    rowIndex++;
                }
            });
        }

        private static float TransformFromMmToPoints(float x)
        {
            return x * Constants.ONE_MM_IN_POINTS;
        }

        private float DetermineMaxWidthOfCell(float x, bool wasTableSplit)
        {
            if (wasTableSplit)
            {
                float y = TransformFromMmToPoints(Constants.CELL_INITIAL_MAX_WIDTH_IN_MM);
                return x > (AvailableWidth - y) ? (AvailableWidth - y) : x;
            }
            return x > AvailableWidth ? AvailableWidth : x;
        }
    }
}
