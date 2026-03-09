using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Quest_PDF_Testing.Sections;
using Quest_PDF_Testing.Models;
using Quest_PDF_Testing.Helpers;

// set your license here:
QuestPDF.Settings.License = LicenseType.Community;

List<SectionBody> sections =
[
    BuildBodySection1(false, true, SectionAnchor.FullWidth),
    BuildBodySection2(true, true, SectionAnchor.Left),
    BuildBodySection3(true, true, SectionAnchor.Right),
    BuildBodySection4(true, false, SectionAnchor.Left),
    BuildBodySection3(false, false, SectionAnchor.FullWidth),
    BuildBodySection2(true, false, SectionAnchor.Right)
];
int sectionsSize = sections.Count;


Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(Constants.BORDER_2, Unit.Centimetre);

        page.Header().AlignTop().ExtendHorizontal().Component(new SectionHeader());
        page.Footer().Component(new SectionFooter());

        page.Content().Column(column =>
        {
            column.Item().Component(BuildTitleSection());
            for (int i = 0; i < sectionsSize; i++)
            {
                if (sections[i].Position == SectionAnchor.FullWidth)
                {
                    column.Item().Component(sections[i]);
                    continue;
                }

                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(leftColumn =>
                    {
                        int j = i;
                        while (i < sectionsSize && sections[i].Position != SectionAnchor.FullWidth)
                        {
                            if (sections[i].Position == SectionAnchor.Left)
                                leftColumn.Item().Component(sections[i]);
                            i++;
                        }
                        i = j;
                    });

                    row.RelativeItem().Column(rightColumn =>
                    {
                        while (i < sectionsSize && sections[i].Position != SectionAnchor.FullWidth)
                        {
                            if (sections[i].Position == SectionAnchor.Right)
                                rightColumn.Item().Component(sections[i]);
                            i++;
                        }
                        i--;
                    });
                });
            }
        });

    });
})
.GeneratePdfAndShow();

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

    var section = new SectionTitle
    {
        Title = titleItem
    };

    return section;
}

static SectionBody BuildBodySection1(bool isHalfPageOnly, bool shouldKeepContentOnSamePage, SectionAnchor anchor)
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
            new KeyValueItem { Key = "Label", Value = "label1, label 2", IsDisplayed = true }
        ]
    };

    var section = new SectionBody
    {
        Body = bodyItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        Position = anchor
    };

    return section;
}

static SectionBody BuildBodySection2(bool isHalfPageOnly, bool shouldKeepContentOnSamePage, SectionAnchor anchor)
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
            new KeyValueItem { Key = "Label", Value = "label1, label 2", IsDisplayed = true }
        ]
    };

    var section = new SectionBody
    {
        Body = bodyItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        Position = anchor
    };

    return section;
}

static SectionBody BuildBodySection3(bool isHalfPageOnly, bool shouldKeepContentOnSamePage, SectionAnchor anchor)
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

    var section = new SectionBody
    {
        Body = bodyItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        Position = anchor
    };

    return section;
}

static SectionBody BuildBodySection4(bool isHalfPageOnly, bool shouldKeepContentOnSamePage, SectionAnchor anchor)
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
            new KeyValueItem { Key = "Label", Value = "label1, label 2", IsDisplayed = true }
        ]
    };

    var section = new SectionBody
    {
        Body = bodyItem,
        IsHalfPageOnly = isHalfPageOnly,
        ShouldKeepContentOnSamePage = shouldKeepContentOnSamePage,
        Position = anchor
    };

    return section;
}