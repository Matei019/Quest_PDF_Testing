namespace Quest_PDF_Testing.Models
{
    public class TableItem
    {
        public string Title { get; set; } = string.Empty;
        public List<CellItem> CellItems { get; set; } = [];
        public List<RowItem> Rows { get; set; } = [];
    }
}
