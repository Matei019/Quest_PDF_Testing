using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Helpers
{
    public static class QuestPdfEnhancedProperties
    {
        public static IContainer PreventPageBreakIf(this IContainer container, bool shouldBreakPage)
        {
            return shouldBreakPage ? container.PreventPageBreak() : container;
        }

        public static IContainer AlignAndPaddingLeftOrRight(this IContainer container, bool shouldAlignLeft, float horizontalPadding)
        {
            return shouldAlignLeft ? container.AlignLeft().PaddingLeft(horizontalPadding) : container.AlignRight().PaddingRight(horizontalPadding);
        }

        public static IContainer BackgroundTableRows(this IContainer container, int rowIndex)
        {
            return rowIndex % 2 == 1 ? container.Background(Color.FromHex(Constants.ODD_TABLE_ROWS_BACKGROUND_COLOR)) : container;
        }

        public static IContainer BorderBottomForLastTableRow(this IContainer container, bool isLastTableRow)
        {
            return isLastTableRow ? container.BorderBottom(Constants.BORDER_2).BorderColor(Color.FromHex(Constants.RED_COLOR_HEX)) : container;
        }
    }
}
