using Quest_PDF_Testing.Models;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class SectionBody : IDynamicComponent
    {
        public BodyItem Body { get; set; }
        public bool IsHalfPageOnly { get; set; } = false;

        public SectionBody()
        {
            Body = new BodyItem();
        }

        public DynamicComponentComposeResult Compose(DynamicContext context)
        {
            var content = context.CreateElement(element =>
            {
                var halfPageWidth = context.AvailableSize.Width / 2;
                if (IsHalfPageOnly)
                {
                    element.MaxWidth(halfPageWidth).Column(column =>
                    {
                        column.Item().PaddingBottom(Constants.PADDING_2).Text(Body.Title).FontSize(Constants.TITLE_FONT_SIZE_35).FontColor(Color.FromHex(Constants.RED_COLOR_HEX));
                        foreach (KeyValueItem keyValueItem in Body.KeyValuePair)
                        {
                            if (keyValueItem.IsDisplayed)
                            {
                                column.Item().PaddingTop(Constants.PADDING_2).Row(pairRow =>
                                {
                                    pairRow.RelativeItem(1).Text(keyValueItem.Key).FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Constants.GRAY_COLOR_HEX_2);
                                    pairRow.RelativeItem(2).Text(keyValueItem.Value).FontSize(Constants.TEXT_FONT_SIZE_10);
                                });
                            }
                        }
                    });
                }
                else
                {
                    element.Column(column =>
                    {
                        column.Item().PaddingBottom(Constants.PADDING_2).Text(Body.Title).FontSize(Constants.TITLE_FONT_SIZE_35).FontColor(Color.FromHex(Constants.RED_COLOR_HEX));
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(innerColumn =>
                            {
                                foreach (KeyValueItem keyValueItem in Body.KeyValuePair.Take(Body.KeyValuePair.Count / 2))
                                {
                                    if (keyValueItem.IsDisplayed) { 
                                        innerColumn.Item().PaddingTop(Constants.PADDING_2).Row(pairRow =>
                                        {
                                            pairRow.RelativeItem(1).Text(keyValueItem.Key).FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Constants.GRAY_COLOR_HEX_2);
                                            pairRow.RelativeItem(2).Text(keyValueItem.Value).FontSize(Constants.TEXT_FONT_SIZE_10);
                                        });
                                    }
                                }
                            });
                            row.RelativeItem().Column(innerColumn =>
                            {
                                foreach (KeyValueItem keyValueItem in Body.KeyValuePair.Skip(Body.KeyValuePair.Count / 2))
                                {
                                    if (keyValueItem.IsDisplayed)
                                    {
                                        innerColumn.Item().PaddingTop(Constants.PADDING_2).Row(pairRow =>
                                        {
                                            pairRow.RelativeItem(1).Text(keyValueItem.Key).FontSize(Constants.TEXT_FONT_SIZE_10).FontColor(Constants.GRAY_COLOR_HEX_2);
                                            pairRow.RelativeItem(2).Text(keyValueItem.Value).FontSize(Constants.TEXT_FONT_SIZE_10);
                                        });
                                    }
                                }
                            });
                        });
                    });
                }
            });

            return new DynamicComponentComposeResult
            {
                Content = content,
                HasMoreContent = false
            };
        }
    }
}
