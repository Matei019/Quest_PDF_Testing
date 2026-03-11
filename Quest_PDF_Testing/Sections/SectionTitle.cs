using Quest_PDF_Testing.Helpers;
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
            container.ShowEntire().PaddingBottom(Constants.PADDING_15).Column(column =>
            {
                column.Item().Text(Title.Title).FontSize(Constants.TITLE_FONT_SIZE_30).FontColor(Color.FromHex(Constants.RED_COLOR_HEX));
                column.Item().AlignLeft().Row(row =>
                {
                    row.AutoItem().PaddingRight(Constants.HALF_POINT_PADDING, Unit.Centimetre).Text(Title.Version).FontSize(Constants.TEXT_FONT_SIZE_10);
                    row.AutoItem().PaddingRight(Constants.HALF_POINT_PADDING, Unit.Centimetre).Text(Title.SerialNumber).FontSize(Constants.TEXT_FONT_SIZE_10);
                    row.AutoItem().Text(Title.Type).FontSize(Constants.TEXT_FONT_SIZE_10);
                });
                column.Item().Text(Title.ControlMessage).FontSize(Constants.CONTROL_FONT_SIZE_25)
                    .FontColor(Title.HasControlSucceded ? Color.FromHex(Constants.SUCCESS_COLOR_HEX) : Color.FromHex(Constants.FAIL_COLOR_HEX));
            });
        }
    }
}
