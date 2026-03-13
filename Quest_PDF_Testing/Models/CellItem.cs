using Quest_PDF_Testing.Helpers;

namespace Quest_PDF_Testing.Models
{
    public class CellItem
    {
        public float WidthMm { get; set; } = Constants.CELL_INITIAL_MAX_WIDTH_IN_MM;
        public string HeaderName { get; set; } = string.Empty;
    }
}
