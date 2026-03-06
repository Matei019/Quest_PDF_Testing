namespace Quest_PDF_Testing.Models
{
    public class TitleItem
    {
        public string Title { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string ControlMessage { get; set; } = string.Empty;
        public bool HasControlSucceded { get; set; } = false;
    }
}
