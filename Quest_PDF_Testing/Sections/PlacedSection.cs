using Quest_PDF_Testing.Helpers;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class PlacedSection
    {
        public IComponent Component { get; set; }
        public SectionAnchor Anchor { get; set; } = SectionAnchor.Left;
        public bool DoesCoverFullPage { get; set; } = true;
    }
}
