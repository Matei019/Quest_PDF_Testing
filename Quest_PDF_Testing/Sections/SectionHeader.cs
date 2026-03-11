using Quest_PDF_Testing.Helpers;
using Quest_PDF_Testing.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class SectionHeader : IComponent
    {
        public HeaderItem Header { get; set; }

        public SectionHeader()
        {
            Header = new HeaderItem();
        }

        public void Compose(IContainer container)
        {
            container.PaddingBottom(Constants.PADDING_1, Unit.Centimetre).Column(column =>
            {
                column.Item().Layers(layers =>
                {
                    layers.Layer().AlignRight().MaxHeight(Header.ImageMaxHeight).MaxWidth(Header.ImageMaxWidth).Image(Header.ImagePath).FitArea();

                    layers.PrimaryLayer().Column(layerColumn =>
                    {
                        layerColumn.Item().AlignLeft().Text(Header.Title1).FontSize(Constants.TEXT_FONT_SIZE_10);
                        layerColumn.Item().AlignLeft().Text(Header.Title2).FontSize(Constants.TEXT_FONT_SIZE_10);
                        layerColumn.Item().AlignLeft().Text(Header.Country).FontSize(Constants.TEXT_FONT_SIZE_10);
                    });
                });
            });
        }
    }
}
