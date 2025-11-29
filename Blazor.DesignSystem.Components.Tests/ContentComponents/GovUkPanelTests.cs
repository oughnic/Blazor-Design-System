using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.ContentComponents;

/// <summary>
/// Unit tests for GovUkPanel component covering rendering and accessibility.
/// </summary>
public class GovUkPanelTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Panel_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkPanel>(parameters => parameters
            .Add(p => p.Title, "Application complete"));

        // Assert
        var panel = cut.Find(".govuk-panel");
        Assert.NotNull(panel);
        Assert.Contains("govuk-panel--confirmation", panel.ClassName);
    }

    [Fact]
    public void Panel_RendersTitle()
    {
        // Arrange & Act
        var cut = Render<GovUkPanel>(parameters => parameters
            .Add(p => p.Title, "Application complete"));

        // Assert
        var title = cut.Find("h1.govuk-panel__title");
        Assert.Equal("Application complete", title.TextContent);
    }

    [Fact]
    public void Panel_RendersChildContent()
    {
        // Arrange & Act
        var cut = Render<GovUkPanel>(parameters => parameters
            .Add(p => p.Title, "Application complete")
            .AddChildContent("<strong>HDJ2123F</strong>"));

        // Assert
        var body = cut.Find(".govuk-panel__body");
        Assert.NotNull(body);
        var strong = cut.Find(".govuk-panel__body strong");
        Assert.Equal("HDJ2123F", strong.TextContent);
    }

    [Fact]
    public void Panel_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkPanel>(parameters => parameters
            .Add(p => p.Title, "Application complete")
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var panel = cut.Find(".govuk-panel");
        Assert.Contains("custom-class", panel.ClassName);
    }

    [Fact]
    public void Panel_NoBody_WhenNoChildContent()
    {
        // Arrange & Act
        var cut = Render<GovUkPanel>(parameters => parameters
            .Add(p => p.Title, "Application complete"));

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find(".govuk-panel__body"));
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void Panel_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkPanel>(parameters => parameters
            .Add(p => p.Title, "Application complete")
            .AddUnmatched("data-test-id", "test-panel")
            .AddUnmatched("id", "my-panel"));

        // Assert
        var panel = cut.Find(".govuk-panel");
        Assert.Equal("test-panel", panel.GetAttribute("data-test-id"));
        Assert.Equal("my-panel", panel.Id);
    }

    #endregion
}
