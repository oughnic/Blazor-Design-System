using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.InteractiveComponents;

/// <summary>
/// Unit tests for GovUkDetails component covering rendering, accessibility, and styling.
/// </summary>
public class GovUkDetailsTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Details_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkDetails>(parameters => parameters
            .Add(p => p.Summary, "Help with nationality")
            .AddChildContent("<p>Details content</p>"));

        // Assert
        var details = cut.Find("details.govuk-details");
        Assert.NotNull(details);
    }

    [Fact]
    public void Details_RendersSummary()
    {
        // Arrange & Act
        var cut = Render<GovUkDetails>(parameters => parameters
            .Add(p => p.Summary, "Help with nationality")
            .AddChildContent("<p>Details content</p>"));

        // Assert
        var summary = cut.Find(".govuk-details__summary");
        Assert.NotNull(summary);
        
        var summaryText = cut.Find(".govuk-details__summary-text");
        Assert.Equal("Help with nationality", summaryText.TextContent);
    }

    [Fact]
    public void Details_RendersContent()
    {
        // Arrange & Act
        var cut = Render<GovUkDetails>(parameters => parameters
            .Add(p => p.Summary, "Help with nationality")
            .AddChildContent("<p>We need to know your nationality</p>"));

        // Assert
        var content = cut.Find(".govuk-details__text");
        Assert.Contains("We need to know your nationality", content.TextContent);
    }

    [Fact]
    public void Details_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkDetails>(parameters => parameters
            .Add(p => p.Summary, "Help")
            .Add(p => p.CssClass, "custom-class")
            .AddChildContent("<p>Content</p>"));

        // Assert
        var details = cut.Find("details.govuk-details");
        Assert.Contains("custom-class", details.ClassName);
    }

    #endregion

    #region Semantic HTML Tests

    [Fact]
    public void Details_UsesNativeDetailsElement()
    {
        // Arrange & Act
        var cut = Render<GovUkDetails>(parameters => parameters
            .Add(p => p.Summary, "Help")
            .AddChildContent("<p>Content</p>"));

        // Assert - native HTML5 details element provides built-in accessibility
        var details = cut.Find("details");
        Assert.NotNull(details);
        
        var summary = cut.Find("summary");
        Assert.NotNull(summary);
    }

    [Fact]
    public void Details_HasCorrectStructure()
    {
        // Arrange & Act
        var cut = Render<GovUkDetails>(parameters => parameters
            .Add(p => p.Summary, "Help")
            .AddChildContent("<p>Content</p>"));

        // Assert
        var details = cut.Find("details");
        var summary = cut.Find("summary");
        var content = cut.Find(".govuk-details__text");
        
        Assert.NotNull(details);
        Assert.NotNull(summary);
        Assert.NotNull(content);
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void Details_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkDetails>(parameters => parameters
            .Add(p => p.Summary, "Help")
            .AddChildContent("<p>Content</p>")
            .AddUnmatched("data-test-id", "test-details")
            .AddUnmatched("id", "my-details"));

        // Assert
        var details = cut.Find("details");
        Assert.Equal("test-details", details.GetAttribute("data-test-id"));
        Assert.Equal("my-details", details.Id);
    }

    #endregion
}
