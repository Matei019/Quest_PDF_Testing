using Quest_PDF_Testing.Helpers;
using Quest_PDF_Testing.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class SectionBody : IComponent
    {
        public BodyItem Body { get; set; }
        public bool IsHalfPageOnly { get; set; } = false;
        public bool ShouldKeepContentOnSamePage { get; set; } = false;
        public SectionAnchor Position { get; set; } = SectionAnchor.FullWidth;

        public SectionBody()
        {
            Body = new BodyItem();
        }

        public void Compose(IContainer container)
        {
            container.ShowEntireIf(ShouldKeepContentOnSamePage).PaddingBottom(Constants.PADDING_15).Column(column =>
            {
                column.Item().PaddingBottom(Constants.PADDING_2).Text(Body.Title).FontSize(Constants.TITLE_FONT_SIZE_30).FontColor(Color.FromHex(Constants.RED_COLOR_HEX));
                
                var keyValuePairs = Body.KeyValuePair.Where(item => item.IsDisplayed).ToList();
                if (IsHalfPageOnly)
                {
                    column.Item().EnsureSpace().Element(c => RenderKeyValuePairs(c, keyValuePairs));
                }
                else
                {
                    column.Item().EnsureSpace().Row(row =>
                    {
                        row.RelativeItem(1).Element(c => RenderKeyValuePairs(c, keyValuePairs.Take(keyValuePairs.Count / 2)));
                        row.RelativeItem(1).Element(c => RenderKeyValuePairs(c, keyValuePairs.Skip(keyValuePairs.Count / 2)));
                    });
                }
            });
        }

        private static void RenderKeyValuePairs(IContainer container, IEnumerable<KeyValueItem> keyValuePairs)
        {
            container.Column(column =>
            {
                foreach (var keyValueItem in keyValuePairs)
                {
                    column.Item().PaddingTop(Constants.PADDING_2).Row(pairRow =>
                    {
                        pairRow.RelativeItem(1).Text(keyValueItem.Key).FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Constants.GRAY_COLOR_HEX_2);
                        pairRow.RelativeItem(2).Text(keyValueItem.Value).FontSize(Constants.TEXT_FONT_SIZE_10);
                    });
                }
            });
        }
    }
}
