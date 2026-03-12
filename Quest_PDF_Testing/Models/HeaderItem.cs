using Quest_PDF_Testing.Helpers;

namespace Quest_PDF_Testing.Models
{
    public class HeaderItem
    {
        public string Title1 { get; set; } = string.Empty;
        public string Title2 { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public float ImageMaxWidth { get; set; } = Constants.HEADER_IMAGE_INITIAL_MAX_WIDTH;
        public float ImageMaxHeight { get; set; } = Constants.HEADER_IMAGE_INITIAL_MAX_HEIGHT;
    }
}
