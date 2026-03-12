using Quest_PDF_Testing.Helpers;
using Quest_PDF_Testing.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class SectionFooter : IComponent
    {
        public FooterItem Footer { get; set; }

        public SectionFooter()
        {
            Footer = new FooterItem();
        }

        public void Compose(IContainer container)
        {
            container.BorderTop(Constants.BORDER_1).BorderColor(Color.FromHex(Constants.GRAY_COLOR_HEX)).Column(column =>
            {
                column.Item().ExtendHorizontal().PaddingTop(Constants.PADDING_10).Layers(layers =>
                {
                    layers.Layer().AlignRight().AlignBottom().Text(x =>
                    {
                        x.Span("Page ").FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                        x.CurrentPageNumber().FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                        x.Span(" of ").FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                        x.TotalPages().FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                    });

                    layers.PrimaryLayer().AlignLeft().Column(layerColumn =>
                    {
                        layerColumn.Item().AlignLeft().Text(Footer.ExportInfo).FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                        layerColumn.Item().AlignLeft().Text(Footer.CompanySite).FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                    });
                });
            });
        }
    }
}
