using Quest_PDF_Testing.Helpers;
using Quest_PDF_Testing.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class SectionImage : IComponent
    {
        public ImageItem Image { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }

        public SectionImage()
        {
            Image = new ImageItem();
        }

        public void Compose(IContainer container)
        {
            container.PreventPageBreak().MaxWidth(Width, Unit.Centimetre).PaddingBottom(Constants.PADDING_15).Border(Constants.BORDER_1).Padding(Constants.PADDING_10)
                .Column(column =>
            {
                column.Item().PaddingBottom(Constants.PADDING_2).Text(Image.Title).FontSize(Constants.TITLE_FONT_SIZE_30)
                    .FontColor(Color.FromHex(Constants.RED_COLOR_HEX));

                column.Item().Height(Height, Unit.Centimetre).Image(Image.ImagePath).FitHeight().FitUnproportionally();
            });
        }
    }
}
