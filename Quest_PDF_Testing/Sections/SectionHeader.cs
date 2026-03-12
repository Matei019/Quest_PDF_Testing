using Quest_PDF_Testing.Helpers;
using Quest_PDF_Testing.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class SectionHeader : IComponent
    {
        public HeaderItem Header { get; set; }
        public bool IsLogoAlignedLeft { get; set; } = false;
        public float LogoPaddingTop { get; set; } = 0f;
        public float LogoPaddingHorizontal { get; set; } = 0f;

        public SectionHeader()
        {
            Header = new HeaderItem();
        }

        public void Compose(IContainer container)
        {
            container.PaddingBottom(Constants.PADDING_1, Unit.Centimetre).Column(column =>
            {
                column.Item().AlignTop().ExtendHorizontal().Layers(layers =>
                {
                    layers.PrimaryLayer().AlignAndPaddingLeftOrRight(IsLogoAlignedLeft, LogoPaddingHorizontal).PaddingTop(LogoPaddingTop)
                        .MaxHeight(Header.ImageMaxHeight).MaxWidth(Header.ImageMaxWidth).Image(Header.ImagePath).FitArea();

                    layers.Layer().Column(layerColumn =>
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
