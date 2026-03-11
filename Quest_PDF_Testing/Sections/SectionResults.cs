using Quest_PDF_Testing.Helpers;
using Quest_PDF_Testing.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Quest_PDF_Testing.Sections
{
    public class SectionResults : IComponent
    {
        public ResultsItem Results { get; set; }
        public bool IsHalfPageOnly { get; set; } = false;
        public bool ShouldKeepContentOnSamePage { get; set; } = false;
        public ResultsView View { get; set; } = ResultsView.ListView;
        public ContentView Content { get; set; } = ContentView.ListView;

        public SectionResults()
        {
            Results = new ResultsItem();
        }

        public void Compose(IContainer container)
        {
            container.PreventPageBreakIf(ShouldKeepContentOnSamePage).PaddingBottom(Constants.PADDING_15).Column(column =>
            {
                column.Item().PaddingBottom(Constants.PADDING_2).Text(Results.Title).FontSize(Constants.TITLE_FONT_SIZE_30).FontColor(Color.FromHex(Constants.RED_COLOR_HEX));

                var keyValueResultPairs = Results.KeyValueResultPair.Where(item => item.IsDisplayed).ToList();
                if (IsHalfPageOnly)
                {
                    if (View == ResultsView.ListView)
                        column.Item().Element(c => RenderListKeyValuePairsForListView(c, keyValueResultPairs));
                    else
                    {
                        var pairsSize = (keyValueResultPairs.Count % 2 == 1) ? (keyValueResultPairs.Count / 2 + 1) : (keyValueResultPairs.Count / 2);
                        column.Item().Row(row =>
                        {
                            row.RelativeItem(1).Element(c => RenderHighlightedKeyValuePairs(c, keyValueResultPairs.Take(pairsSize)));
                            row.RelativeItem(1).Element(c => RenderHighlightedKeyValuePairs(c, keyValueResultPairs.Skip(pairsSize)));
                        });
                    }
                }
                else if (View == ResultsView.ListView)
                {
                    if (Content == ContentView.ListView)
                        column.Item().Element(c => RenderListKeyValuePairsForListView(c, keyValueResultPairs));
                    else
                    {
                        var pairsSize = (keyValueResultPairs.Count % 2 == 1) ? (keyValueResultPairs.Count / 2 + 1) : (keyValueResultPairs.Count / 2);
                        column.Item().Row(row =>
                        {
                            row.RelativeItem(1).Element(c => RenderListKeyValuePairsForListView(c, keyValueResultPairs.Take(pairsSize)));
                            row.RelativeItem(1).Element(c => RenderListKeyValuePairsForListView(c, keyValueResultPairs.Skip(pairsSize)));
                        });
                    }

                }
                else if (View == ResultsView.HighlightedAndListView)
                {
                    var highlightedKeyValuePair = keyValueResultPairs.Where(item => item.IsHighlightedItem).ToList();
                    var nonHighlightedKeyValuePair = keyValueResultPairs.Where(item => !item.IsHighlightedItem).ToList();

                    column.Item().Row(row =>
                    {
                        var highlightedPairsSize = (highlightedKeyValuePair.Count % 2 == 1) ? (highlightedKeyValuePair.Count / 2 + 1) : (highlightedKeyValuePair.Count / 2);
                        var hasMultipleElements = highlightedKeyValuePair.Skip(highlightedPairsSize).Any();

                        row.RelativeItem(1).Element(c => RenderHighlightedKeyValuePairs(c, highlightedKeyValuePair.Take(highlightedPairsSize)));
                        if (hasMultipleElements)
                            row.RelativeItem(1).Element(c => RenderHighlightedKeyValuePairs(c, highlightedKeyValuePair.Skip(highlightedPairsSize)));

                        row.RelativeItem(hasMultipleElements ? 1 : 2).Element(c => RenderListKeyValuePairsForListView(c, nonHighlightedKeyValuePair));
                    });
                }
                else if (View == ResultsView.HighlightedOnlyView)
                {
                    column.Item().Row(row =>
                    {
                        var pairsSize = keyValueResultPairs.Count;
                        int numberOfColumns = (int)Math.Ceiling((double)pairsSize / 2);
                        if (pairsSize > Constants.MAX_COLUMNS_FOR_HIGHLIGHTED_ITEMS)
                            numberOfColumns = Math.Min(pairsSize, Constants.MAX_COLUMNS_FOR_HIGHLIGHTED_ITEMS);

                        int itemsPerColumn = (int)Math.Ceiling((double)pairsSize / numberOfColumns);
                        for (int i = 0; i < numberOfColumns; i++)
                        {
                            var columnItems = keyValueResultPairs.Skip(i * itemsPerColumn).Take(itemsPerColumn);
                            if (columnItems.Any())
                                row.RelativeItem(1).Element(c => RenderHighlightedKeyValuePairs(c, columnItems));
                        }
                    });
                }
            });
        }

        private void RenderListKeyValuePairsForListView(IContainer container, IEnumerable<KeyValueItem> keyValuePairs)
        {
            container.Column(column =>
            {
                foreach (var keyValueItem in keyValuePairs)
                    column.Item().EnsureSpace().PaddingTop(Constants.PADDING_3).Row(pairRow =>
                    {
                        pairRow.RelativeItem(1).PaddingRight(Constants.PADDING_5).Text(keyValueItem.Key).FontSize(Constants.TEXT_FONT_SIZE_10)
                            .FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX_2));
                        pairRow.RelativeItem(IsHalfPageOnly ? 1 : 2).AlignMiddle().Text(keyValueItem.Value).FontSize(Constants.TEXT_FONT_SIZE_10);
                    });
            });
        }

        private static void RenderHighlightedKeyValuePairs(IContainer container, IEnumerable<KeyValueItemResult> keyValuePairs)
        {
            container.Column(column =>
            {
                foreach (var keyValueItem in keyValuePairs)
                {
                    column.Item().EnsureSpace().PaddingRight(Constants.PADDING_2).Column(innerColumn =>
                    {
                        innerColumn.Item().PaddingTop(Constants.PADDING_3).Text(keyValueItem.Key).FontSize(Constants.TEXT_FONT_SIZE_10)
                            .FontColor(Color.FromHex(Constants.GRAY_COLOR_HEX_2));
                        innerColumn.Item().Text(keyValueItem.Value).FontSize(Constants.TEXT_FONT_SIZE_18);
                    });
                }
            });
        }
    }
}
