using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Quest_PDF_Testing.Sections;
using Quest_PDF_Testing.Models;
using Quest_PDF_Testing;

// set your license here:
QuestPDF.Settings.License = LicenseType.Community;

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
            column.Item().Dynamic(BuildBodySection());
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

static IDynamicComponent BuildBodySection()
{
    BodyItem bodyItem = new()
    {
        Title = "Sample information",
        KeyValuePair =
        [
            new KeyValueItem { Key = "Name", Value = "Another Sample for Measurement_Type", IsDisplayed = true },
            new KeyValueItem { Key = "Sample position", Value = "59", IsDisplayed = true },
            new KeyValueItem { Key = "Sample weight", Value = "40.042g", IsDisplayed = false },
            new KeyValueItem { Key = "Location", Value = "Lab A", IsDisplayed = true },
            new KeyValueItem { Key = "End date", Value = "2026-03-06", IsDisplayed = true },
            new KeyValueItem { Key = "User", Value = "John Doe", IsDisplayed = true },
            new KeyValueItem { Key = "Batch number", Value = "8q7vtbry978241v9", IsDisplayed = false },
            new KeyValueItem { Key = "Note", Value = "Whatever the Measurement_Type measures", IsDisplayed = false },
            new KeyValueItem { Key = "Material density", Value = "1.27 g.cm^3", IsDisplayed = false },
            new KeyValueItem { Key = "Start date", Value = "2026-03-06", IsDisplayed = false },
            new KeyValueItem { Key = "Duration", Value = "10 min 23 s", IsDisplayed = true },
            new KeyValueItem { Key = "Label", Value = "label1, label 2", IsDisplayed = false }
        ]
    };

    var section = new SectionBody
    {
        Body = bodyItem,
        IsHalfPageOnly = false
    };

    return section;
}