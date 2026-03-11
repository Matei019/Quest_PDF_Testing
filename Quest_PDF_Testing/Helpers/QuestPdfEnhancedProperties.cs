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
    }
}
