using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.ContentComponents;

/// <summary>
/// Unit tests for GovUkInsetText component covering rendering.
/// </summary>
public class GovUkInsetTextTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void InsetText_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkInsetText>(parameters => parameters
            .AddChildContent("This is some inset text."));

        // Assert
        var inset = cut.Find(".govuk-inset-text");
        Assert.NotNull(inset);
    }

    [Fact]
    public void InsetText_RendersContent()
    {
        // Arrange & Act
        var cut = Render<GovUkInsetText>(parameters => parameters
            .AddChildContent("It can take up to 8 weeks to register a lasting power of attorney."));

        // Assert
        var inset = cut.Find(".govuk-inset-text");
        Assert.Contains("It can take up to 8 weeks to register a lasting power of attorney.", inset.TextContent);
    }

    [Fact]
    public void InsetText_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkInsetText>(parameters => parameters
            .Add(p => p.CssClass, "custom-class")
            .AddChildContent("Inset text content."));

        // Assert
        var inset = cut.Find(".govuk-inset-text");
        Assert.Contains("custom-class", inset.ClassName);
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void InsetText_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkInsetText>(parameters => parameters
            .AddChildContent("Inset text content.")
            .AddUnmatched("data-test-id", "test-inset")
            .AddUnmatched("id", "my-inset"));

        // Assert
        var inset = cut.Find(".govuk-inset-text");
        Assert.Equal("test-inset", inset.GetAttribute("data-test-id"));
        Assert.Equal("my-inset", inset.Id);
    }

    #endregion
}
