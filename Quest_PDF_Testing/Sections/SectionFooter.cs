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
                column.Item().PaddingTop(Constants.PADDING_10).Layers(layers =>
                {
                    layers.Layer().AlignRight().AlignBottom().Text(x =>
                    {
                        x.Span("Page ").FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                        x.CurrentPageNumber().FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                        x.Span(" of ").FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                        x.TotalPages().FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                    });

                    layers.PrimaryLayer().AlignLeft().Column(layerColumn =>
                    {
                        layerColumn.Item().AlignLeft().Text(Footer.ExportInfo).FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                        layerColumn.Item().AlignLeft().Text(Footer.CompanySite).FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX));
                    });
                });
            });
        }
    }
}
