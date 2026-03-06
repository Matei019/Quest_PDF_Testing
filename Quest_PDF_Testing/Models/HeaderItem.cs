namespace Quest_PDF_Testing.Models
{
    public class HeaderItem
    {
        public string Title1 { get; set; } = "Title 1";
        public string Title2 { get; set; } = "Title 2";
        public string Country { get; set; } = "Country";
        public string ImagePath { get; set; } = Path.Combine(AppContext.BaseDirectory , "Screenshot 2026-02-02 152908.png");
        public float ImageMaxWidth { get; set; } = 250f;
        public float ImageMaxHeight { get; set; } = 100f;
    }
}
