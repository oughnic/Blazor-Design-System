using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.ContentComponents;

/// <summary>
/// Unit tests for GovUkTable component covering rendering, structure, and accessibility.
/// </summary>
public class GovUkTableTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Table_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>();

        // Assert
        var table = cut.Find("table.govuk-table");
        Assert.NotNull(table);
    }

    [Fact]
    public void Table_RendersCaption()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>(parameters => parameters
            .Add(p => p.Caption, "Dates and amounts"));

        // Assert
        var caption = cut.Find("caption.govuk-table__caption");
        Assert.Equal("Dates and amounts", caption.TextContent);
    }

    [Fact]
    public void Table_NoCaption_WhenNotProvided()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>();

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find("caption"));
    }

    [Fact]
    public void Table_CaptionAppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>(parameters => parameters
            .Add(p => p.Caption, "Dates and amounts")
            .Add(p => p.CaptionCssClass, "govuk-table__caption--m"));

        // Assert
        var caption = cut.Find("caption.govuk-table__caption");
        Assert.Contains("govuk-table__caption--m", caption.ClassName);
    }

    [Fact]
    public void Table_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>(parameters => parameters
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var table = cut.Find("table.govuk-table");
        Assert.Contains("custom-class", table.ClassName);
    }

    #endregion

    #region Structure Tests

    [Fact]
    public void Table_RendersHead()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>(parameters => parameters
            .Add(p => p.Head, (RenderFragment)(b => 
                b.AddMarkupContent(0, "<tr class=\"govuk-table__row\"><th class=\"govuk-table__header\">Header</th></tr>"))));

        // Assert
        var thead = cut.Find("thead.govuk-table__head");
        Assert.NotNull(thead);
    }

    [Fact]
    public void Table_RendersBody()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>(parameters => parameters
            .AddChildContent("<tr class=\"govuk-table__row\"><td class=\"govuk-table__cell\">Cell</td></tr>"));

        // Assert
        var tbody = cut.Find("tbody.govuk-table__body");
        Assert.NotNull(tbody);
    }

    [Fact]
    public void Table_CompleteStructure()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>(parameters => parameters
            .Add(p => p.Caption, "Table caption")
            .Add(p => p.Head, (RenderFragment)(b => 
                b.AddMarkupContent(0, "<tr class=\"govuk-table__row\"><th scope=\"col\" class=\"govuk-table__header\">Header 1</th><th scope=\"col\" class=\"govuk-table__header\">Header 2</th></tr>")))
            .AddChildContent("<tr class=\"govuk-table__row\"><td class=\"govuk-table__cell\">Cell 1</td><td class=\"govuk-table__cell\">Cell 2</td></tr>"));

        // Assert
        Assert.NotNull(cut.Find("table.govuk-table"));
        Assert.NotNull(cut.Find("caption.govuk-table__caption"));
        Assert.NotNull(cut.Find("thead.govuk-table__head"));
        Assert.NotNull(cut.Find("tbody.govuk-table__body"));
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void Table_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkTable>(parameters => parameters
            .AddUnmatched("data-test-id", "test-table")
            .AddUnmatched("id", "my-table"));

        // Assert
        var table = cut.Find("table.govuk-table");
        Assert.Equal("test-table", table.GetAttribute("data-test-id"));
        Assert.Equal("my-table", table.Id);
    }

    #endregion
}
