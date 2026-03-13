using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Quest_PDF_Testing.Sections;
using Quest_PDF_Testing.Models;
using Quest_PDF_Testing.Helpers;

// set your license here:
QuestPDF.Settings.License = LicenseType.Community;


List<SectionBody> bodySections =
[
    (SectionBody)BuildBodySection1(isHalfPageOnly: false, shouldKeepContentOnSamePage: true, view: ContentView.GridView),
    (SectionBody)BuildBodySection2(isHalfPageOnly: true, shouldKeepContentOnSamePage: true),
    (SectionBody)BuildBodySection3(isHalfPageOnly: true, shouldKeepContentOnSamePage: true),
    (SectionBody)BuildBodySection4(isHalfPageOnly: true, shouldKeepContentOnSamePage: false),
    (SectionBody)BuildBodySection3(isHalfPageOnly: false, shouldKeepContentOnSamePage: false, view: ContentView.ListView),
    (SectionBody)BuildBodySection2(isHalfPageOnly: true, shouldKeepContentOnSamePage: false)
];
List<SectionResults> resultsSections =
[
    (SectionResults)BuildResultsSection1(isHalfPageOnly: false, shouldKeepContentOnSamePage: false, view: ResultsView.HighlightedAndListView),
    (SectionResults)BuildResultsSection1(isHalfPageOnly: true, shouldKeepContentOnSamePage: false),
    (SectionResults)BuildResultsSection1(isHalfPageOnly: true, shouldKeepContentOnSamePage: true, view: ResultsView.HighlightedOnlyView),
    (SectionResults)BuildResultsSection1(isHalfPageOnly: false, shouldKeepContentOnSamePage: false, contentView: ContentView.GridView),
    (SectionResults)BuildResultsSection1(isHalfPageOnly: false, shouldKeepContentOnSamePage: false),
    (SectionResults)BuildResultsSection2(isHalfPageOnly: false, shouldKeepContentOnSamePage: true, view: ResultsView.HighlightedOnlyView)
];
List<SectionTable> tableSections =
[
    (SectionTable)BuildTableComponent1(isHalfPageOnly: false, shouldKeepContentOnSamePage: true)
];

List<PlacedSection> sections =
[
    new PlacedSection { Component = BuildTitleSection() },
    new PlacedSection { Component = bodySections[0], DoesCoverFullPage = !bodySections[0].IsHalfPageOnly },
    new PlacedSection { Component = bodySections[1], Anchor = SectionAnchor.Left, DoesCoverFullPage = !bodySections[1].IsHalfPageOnly },
    new PlacedSection { Component = bodySections[2], Anchor = SectionAnchor.Right, DoesCoverFullPage = !bodySections[2].IsHalfPageOnly },
    new PlacedSection { Component = bodySections[3], Anchor = SectionAnchor.Left, DoesCoverFullPage = !bodySections[3].IsHalfPageOnly },
    new PlacedSection { Component = bodySections[4], DoesCoverFullPage = !bodySections[4].IsHalfPageOnly },
    new PlacedSection { Component = bodySections[5], Anchor = SectionAnchor.Right, DoesCoverFullPage = !bodySections[5].IsHalfPageOnly },
    new PlacedSection { Component = resultsSections[0], DoesCoverFullPage = !resultsSections[0].IsHalfPageOnly },
    new PlacedSection { Component = resultsSections[1], Anchor = SectionAnchor.Left, DoesCoverFullPage = !resultsSections[1].IsHalfPageOnly },
    new PlacedSection { Component = resultsSections[2], Anchor = SectionAnchor.Right, DoesCoverFullPage = !resultsSections[2].IsHalfPageOnly },
    new PlacedSection { Component = resultsSections[3], DoesCoverFullPage = !resultsSections[3].IsHalfPageOnly },
    new PlacedSection { Component = resultsSections[4], DoesCoverFullPage = !resultsSections[4].IsHalfPageOnly },
    new PlacedSection { Component = resultsSections[5], DoesCoverFullPage = !resultsSections[5].IsHalfPageOnly },
    new PlacedSection { Component = BuildImageSection(10f, 15f, "Content_Image_Placeholder.png") },
    new PlacedSection { Component = tableSections[0], DoesCoverFullPage = !tableSections[0].IsHalfPageOnly }
];


Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(Constants.BORDER_2, Unit.Centimetre);

        page.Header().Component(BuildHeaderSection(50f, 100f, 0f, 0f, "Header_Image_Placeholder.png"));
        page.Footer().Component(BuildFooterSection());

        page.Content().Column(column =>
        {
            List<PlacedSection> halfPageSections = [];
            foreach (var section in sections)
            {
                bool coversFullPageWidth = section.Component is SectionTitle || section.Component is SectionImage || section.DoesCoverFullPage;
                if (coversFullPageWidth)
                {
                    column.Item().Element(c => RenderHalfPageComponents(c, halfPageSections));
                    column.Item().Component(section.Component);
                }
                else
                    halfPageSections.Add(section);
            }

            if (halfPageSections.Count > 0)
                column.Item().Element(c => RenderHalfPageComponents(c, halfPageSections));
        });
    });
})
.GeneratePdfAndShow();


static void RenderHalfPageComponents(IContainer container, List<PlacedSection> sections)
{
    if (sections.Count == 0)
        return;

    container.Column(column =>
    {
        column.Item().Row(row =>
        {
            row.RelativeItem().PaddingRight(Constants.PADDING_5).Column(leftColumn =>
            {
                foreach (PlacedSection leftSection in sections.Where(section => section.Anchor == SectionAnchor.Left))
                    leftColumn.Item().Component(leftSection.Component);
            });

            row.RelativeItem().Column(rightColumn =>
            {
                foreach (PlacedSection rightSection in sections.Where(section => section.Anchor == SectionAnchor.Right))
                    rightColumn.Item().Component(rightSection.Component);
            });
        });
    });

    sections.Clear();
}


static IComponent BuildHeaderSection(float maxHeight, float maxWidth, float logoTopPadding, float logoHorizontalPadding, string imageName)
{
    HeaderItem headerItem = new()
    {
        Title1 = "Company name 1",
        Title2 = "Company name 2",
        Country = "Company country",
        ImagePath = Path.Combine(AppContext.BaseDirectory, imageName),
        ImageMaxHeight = maxHeight,
        ImageMaxWidth = maxWidth
    };

    SectionHeader section = new()
    {
        Header = headerItem,
        IsLogoAlignedLeft = false,
        LogoPaddingTop = logoTopPadding,
        LogoPaddingHorizontal = logoHorizontalPadding
    };

    return section;
}

static IComponent BuildFooterSection()
{
    FooterItem footerItem = new()
    {
        ExportInfo = "Exported on 12/3/2026 12:06:18 PM by John Doe",
        CompanySite = "www.company-site.com"
    };

    SectionFooter section = new()
    {
        Footer = footerItem
    };

    return section;
}

static IComponent BuildTitleSection()
{
    TitleItem titleItem = new()
    {
        Title = "Measurement report",
        Version = "Version 2026.2.21",
        SerialNumber = "Serial number 26-32-Z2UTMYD",
        Type = "Measurement_Type #K68HW",
        ControlMessage = "✓ Quality control passed",
        HasControlSucceded = true
    };

    SectionTitle section = new()
    {
        Title = titleItem
    };

    return section;
}

static IComponent BuildBodySection1(bool isHalfPageOnly, bool shouldKeepContentOnSamePage, ContentView view = ContentView.ListView)
{
    BodyItem bodyItem = new()
    {
        Title = "Sample information",
        KeyValuePair =
        [
            new KeyValueItem { Key = "Name", Value = "Another Sample for Measurement Type", IsDisplayed = true },
            new KeyValueItem { Key = "Sample position", Value = "59", IsDisplayed = true },
            new KeyValueItem { Key = "Sample weight", Value = "40.042g", IsDisplayed = true },
            new KeyValueItem { Key = "Location", Value = "Lab A", IsDisplayed = true },
            new KeyValueItem { Key = "End date", Value = "2026-03-06", IsDisplayed = true },
            new KeyValueItem { Key = "User", Value = "John Doe", IsDisplayed = true },
            new KeyValueItem { Key = "Batch number", Value = "8q7vtbry978241v9", IsDisplayed = true },
            new KeyValueItem { Key = "Note", Value = "Whatever the Measurement Type measures", IsDisplayed = true },
            new KeyValueItem { Key = "Material density", Value = "1.27 g/cm³", IsDisplayed = true },
            new KeyValueItem { Key = "Start date", Value = "2026-03-06", IsDisplayed = true },
            new KeyValueItem { Key = "Duration", Value = "10 min 23 s", IsDisplayed = true },
            new KeyValueItem { Key = "Label", Value = "label1, label2", IsDisplayed = true }
        ]
    };

    SectionBody section = new()
    {
        Body = bodyItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        View = view
    };

    return section;
}

static IComponent BuildBodySection2(bool isHalfPageOnly, bool shouldKeepContentOnSamePage, ContentView view = ContentView.ListView)
{
    BodyItem bodyItem = new()
    {
        Title = "Sample information",
        KeyValuePair =
        [
            new KeyValueItem { Key = "Name", Value = "Another Sample for Measurement Type", IsDisplayed = true },
            new KeyValueItem { Key = "Sample position", Value = "59", IsDisplayed = false },
            new KeyValueItem { Key = "Sample weight", Value = "40.042g", IsDisplayed = false },
            new KeyValueItem { Key = "Location", Value = "Lab A", IsDisplayed = false },
            new KeyValueItem { Key = "End date", Value = "2026-03-06", IsDisplayed = false },
            new KeyValueItem { Key = "User", Value = "John Doe", IsDisplayed = false },
            new KeyValueItem { Key = "Batch number", Value = "8q7vtbry978241v9", IsDisplayed = false },
            new KeyValueItem { Key = "Note", Value = "Whatever the Measurement Type measures", IsDisplayed = false },
            new KeyValueItem { Key = "Material density", Value = "1.27 g/cm³", IsDisplayed = false },
            new KeyValueItem { Key = "Start date", Value = "2026-03-06", IsDisplayed = false },
            new KeyValueItem { Key = "Duration", Value = "10 min 23 s", IsDisplayed = false },
            new KeyValueItem { Key = "Label", Value = "label1, label2", IsDisplayed = true }
        ]
    };

    SectionBody section = new()
    {
        Body = bodyItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        View = view
    };

    return section;
}

static IComponent BuildBodySection3(bool isHalfPageOnly, bool shouldKeepContentOnSamePage, ContentView view = ContentView.ListView)
{
    BodyItem bodyItem = new()
    {
        Title = "Measurement setup",
        KeyValuePair =
        [
            new KeyValueItem { Key = "Method name", Value = "method1", IsDisplayed = true },
            new KeyValueItem { Key = "Temperature", Value = "160.0000°C", IsDisplayed = true },
            new KeyValueItem { Key = "Expected Moisture", Value = "0.2000", IsDisplayed = true },
            new KeyValueItem { Key = "Expected Weight", Value = "5.0000g", IsDisplayed = true },
            new KeyValueItem { Key = "Measurement Gradient", Value = "1.0000°C", IsDisplayed = true },
            new KeyValueItem { Key = "Measurement Upper Limit", Value = "0.0000°C", IsDisplayed = true },
            new KeyValueItem { Key = "Measurement Lower Limit", Value = "0.0000°C", IsDisplayed = true },
            new KeyValueItem { Key = "Density", Value = "1.2000", IsDisplayed = true },
            new KeyValueItem { Key = "Type", Value = "Measurement Type", IsDisplayed = true },
            new KeyValueItem { Key = "Manufacturer", Value = "Company Name", IsDisplayed = true },
            new KeyValueItem { Key = "Name", Value = "measurement_type", IsDisplayed = true }
        ]
    };

    SectionBody section = new()
    {
        Body = bodyItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        View = view
    };

    return section;
}

static IComponent BuildBodySection4(bool isHalfPageOnly, bool shouldKeepContentOnSamePage, ContentView view = ContentView.ListView)
{
    BodyItem bodyItem = new()
    {
        Title = "Sample information",
        KeyValuePair =
        [
            new KeyValueItem { Key = "Name", Value = "Another Sample for Measurement Type", IsDisplayed = true },
            new KeyValueItem { Key = "Sample position", Value = "59", IsDisplayed = true },
            new KeyValueItem { Key = "Sample weight", Value = "40.042g", IsDisplayed = true },
            new KeyValueItem { Key = "Location", Value = "Lab A", IsDisplayed = true },
            new KeyValueItem { Key = "End date", Value = "2026-03-06", IsDisplayed = true },
            new KeyValueItem { Key = "User", Value = "John Doe", IsDisplayed = true },
            new KeyValueItem { Key = "Batch number", Value = "8q7vtbry978241v9", IsDisplayed = true },
            new KeyValueItem { Key = "Note", Value = "Whatever the Measurement Type measures", IsDisplayed = true },
            new KeyValueItem { Key = "Material density", Value = "1.27 g/cm³", IsDisplayed = true },
            new KeyValueItem { Key = "Start date", Value = "2026-03-06", IsDisplayed = true },
            new KeyValueItem { Key = "Duration", Value = "10 min 23 s", IsDisplayed = true },
            new KeyValueItem { Key = "Label", Value = "label1, label2", IsDisplayed = true }
        ]
    };

    SectionBody section = new()
    {
        Body = bodyItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        View = view
    };

    return section;
}

static IComponent BuildResultsSection1
    (bool isHalfPageOnly,
    bool shouldKeepContentOnSamePage,
    ResultsView view = ResultsView.ListView,
    ContentView contentView = ContentView.ListView)
{
    ResultsItem resultsItem = new()
    {
        Title = "Results",
        KeyValueResultPair =
        [
            new KeyValueItemResult { Key = "Moisture", Value = "11.7650 g", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Absolute moisture", Value = "235.3184 g", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Standard deviation (density)", Value = "0.0002 g/cm³", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Average volume", Value = "39.1542 g/cm³", IsDisplayed = true, IsHighlightedItem = false },
            new KeyValueItemResult { Key = "True density", Value = "2.1305 g/cm³", IsDisplayed = true, IsHighlightedItem = false },
            new KeyValueItemResult { Key = "Standard deviation (volume)", Value = "0.0003 g/cm³", IsDisplayed = true, IsHighlightedItem = true }
        ]
    };

    SectionResults section = new()
    {
        Results = resultsItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        View = view,
        Content = contentView
    };

    return section;
}

static IComponent BuildResultsSection2
    (bool isHalfPageOnly,
    bool shouldKeepContentOnSamePage,
    ResultsView view = ResultsView.ListView,
    ContentView contentView = ContentView.ListView)
{
    ResultsItem resultsItem = new()
    {
        Title = "Results",
        KeyValueResultPair =
        [
            new KeyValueItemResult { Key = "Moisture", Value = "11.7650 g", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Absolute moisture", Value = "235.3184 g", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Standard deviation (density)", Value = "0.0002 g/cm³", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Average volume", Value = "39.1542 g/cm³", IsDisplayed = true, IsHighlightedItem = false },
            new KeyValueItemResult { Key = "True density", Value = "2.1305 g/cm³", IsDisplayed = true, IsHighlightedItem = false },
            new KeyValueItemResult { Key = "Standard deviation (volume)", Value = "0.0003 g/cm³", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Standard deviation (volume)", Value = "0.0003 g/cm³", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Standard deviation (volume)", Value = "0.0003 g/cm³", IsDisplayed = true, IsHighlightedItem = true },
            new KeyValueItemResult { Key = "Standard deviation (volume)", Value = "0.0003 g/cm³", IsDisplayed = true, IsHighlightedItem = true },
        ]
    };

    SectionResults section = new()
    {
        Results = resultsItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        View = view,
        Content = contentView
    };

    return section;
}

static IComponent BuildImageSection(float height, float width, string imageName)
{
    ImageItem imageItem = new()
    {
        Title = "Moisture over time",
        ImagePath = Path.Combine(AppContext.BaseDirectory, imageName)
    };

    SectionImage section = new()
    {
        Image = imageItem,
        Height = height,
        Width = width
    };

    return section;
}

static IComponent BuildTableComponent1(bool isHalfPageOnly, bool shouldKeepContentOnSamePage)
{
    List<CellItem> cellItems =
    [
        new CellItem { HeaderName = "Run", WidthMm = 12f },
        new CellItem { HeaderName = "Volume", WidthMm = 30f },
        new CellItem { HeaderName = "Density", WidthMm = 90f },
        new CellItem { HeaderName = "Temperature", WidthMm = 40f }
    ];
    List<RowItem> rowItems =
    [
        new RowItem { Items = ["A", "43.1859 g/cm³", "2.1445 g/cm³", "24.01 °C"] },
        new RowItem { Items = ["B", "43.1835 g/cm³", "2.1433 g/cm³", "24.02 °C"] },
        new RowItem { Items = ["C", "43.1875 g/cm³", "2.1412 g/cm³", "24.03 °C"] },
        new RowItem { Items = ["D", "43.1823 g/cm³", "2.1439 g/cm³", "24.04 °C"] }
    ];

    TableItem tableItem = new()
    {
        Title = "Run data",
        CellItems = cellItems,
        Rows = rowItems
    };

    SectionTable section = new()
    {
        Table = tableItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        AvailableWidth = PageSizes.A4.Width - (Constants.BORDER_2 * Constants.ONE_CM_IN_POINTS) * 2
    };

    return section;
}