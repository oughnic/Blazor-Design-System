using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.ContentComponents;

/// <summary>
/// Unit tests for GovUkWarningText component covering rendering and accessibility.
/// </summary>
public class GovUkWarningTextTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void WarningText_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .AddChildContent("You can be fined up to £5,000 if you do not register."));

        // Assert
        var warning = cut.Find(".govuk-warning-text");
        Assert.NotNull(warning);
    }

    [Fact]
    public void WarningText_RendersIcon()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .AddChildContent("Warning message"));

        // Assert
        var icon = cut.Find(".govuk-warning-text__icon");
        Assert.Equal("!", icon.TextContent);
    }

    [Fact]
    public void WarningText_IconIsHiddenFromScreenReaders()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .AddChildContent("Warning message"));

        // Assert
        var icon = cut.Find(".govuk-warning-text__icon");
        Assert.Equal("true", icon.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void WarningText_RendersContent()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .AddChildContent("You can be fined up to £5,000 if you do not register."));

        // Assert
        var text = cut.Find(".govuk-warning-text__text");
        Assert.Contains("You can be fined up to £5,000 if you do not register.", text.TextContent);
    }

    [Fact]
    public void WarningText_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .Add(p => p.CssClass, "custom-class")
            .AddChildContent("Warning message"));

        // Assert
        var warning = cut.Find(".govuk-warning-text");
        Assert.Contains("custom-class", warning.ClassName);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void WarningText_HasVisuallyHiddenAssistiveText()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .AddChildContent("Warning message"));

        // Assert
        var assistiveText = cut.Find(".govuk-warning-text__text .govuk-visually-hidden");
        Assert.Equal("Warning", assistiveText.TextContent);
    }

    [Fact]
    public void WarningText_CustomAssistiveText()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .Add(p => p.AssistiveText, "Important")
            .AddChildContent("Warning message"));

        // Assert
        var assistiveText = cut.Find(".govuk-warning-text__text .govuk-visually-hidden");
        Assert.Equal("Important", assistiveText.TextContent);
    }

    #endregion

    #region Strong Element Tests

    [Fact]
    public void WarningText_ContentIsWrappedInStrong()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .AddChildContent("Warning message"));

        // Assert
        var strong = cut.Find("strong.govuk-warning-text__text");
        Assert.NotNull(strong);
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void WarningText_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkWarningText>(parameters => parameters
            .AddChildContent("Warning message")
            .AddUnmatched("data-test-id", "test-warning")
            .AddUnmatched("id", "my-warning"));

        // Assert
        var warning = cut.Find(".govuk-warning-text");
        Assert.Equal("test-warning", warning.GetAttribute("data-test-id"));
        Assert.Equal("my-warning", warning.Id);
    }

    #endregion
}
