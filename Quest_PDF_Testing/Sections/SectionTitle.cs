using Quest_PDF_Testing.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class SectionTitle : IComponent
    {
        public TitleItem Title { get; set; }

        public SectionTitle()
        {
            Title = new TitleItem();
        }

        public void Compose(IContainer container)
        {
            container.PaddingBottom(Constants.PADDING_15).Column(column =>
            {
                column.Item().Text(Title.Title).FontSize(Constants.TITLE_FONT_SIZE_35).FontColor(Color.FromHex(Constants.RED_COLOR_HEX));
                column.Item().AlignLeft().Row(row =>
                {
                    row.AutoItem().PaddingRight(Constants.HALF_POINT_PADDING, Unit.Centimetre).Text(Title.Version).FontSize(Constants.TEXT_FONT_SIZE_10);
                    row.AutoItem().PaddingRight(Constants.HALF_POINT_PADDING, Unit.Centimetre).Text(Title.SerialNumber).FontSize(Constants.TEXT_FONT_SIZE_10);
                    row.AutoItem().Text(Title.Type).FontSize(Constants.TEXT_FONT_SIZE_10);
                });
                column.Item().Text(Title.ControlMessage).FontSize(Constants.CONTROL_FONT_SIZE_30)
                    .FontColor(Title.HasControlSucceded ? Color.FromHex("#06c709") : Color.FromHex("#a6080b"));
            });
        }
    }
}
