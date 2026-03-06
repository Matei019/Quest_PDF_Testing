namespace Quest_PDF_Testing.Models
{
    public class TestItem
    {
        public string RandomText { get; set; } = string.Empty;
        public int PercentagePageToOccupy { get; set; }
        public bool WasPlacedInDocument { get; set; } = false;
    }
}
