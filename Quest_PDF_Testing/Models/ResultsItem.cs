namespace Quest_PDF_Testing.Models
{
    public class ResultsItem
    {
        public string Title { get; set; } = string.Empty;
        public List<KeyValueItemResult> KeyValueResultPair { get; set; } = [];
    }
}
