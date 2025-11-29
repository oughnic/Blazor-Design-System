using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.ContentComponents;

/// <summary>
/// Unit tests for GovUkTag component covering rendering, styling, and customization.
/// </summary>
public class GovUkTagTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Tag_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTag>(parameters => parameters
            .Add(p => p.Text, "Active"));

        // Assert
        var tag = cut.Find("strong.govuk-tag");
        Assert.NotNull(tag);
    }

    [Fact]
    public void Tag_RendersText()
    {
        // Arrange & Act
        var cut = Render<GovUkTag>(parameters => parameters
            .Add(p => p.Text, "COMPLETED"));

        // Assert
        var tag = cut.Find("strong.govuk-tag");
        Assert.Equal("COMPLETED", tag.TextContent);
    }

    [Fact]
    public void Tag_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTag>(parameters => parameters
            .Add(p => p.Text, "Active")
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var tag = cut.Find("strong.govuk-tag");
        Assert.Contains("custom-class", tag.ClassName);
    }

    #endregion

    #region Color Variants Tests

    [Theory]
    [InlineData("grey")]
    [InlineData("green")]
    [InlineData("turquoise")]
    [InlineData("blue")]
    [InlineData("light-blue")]
    [InlineData("purple")]
    [InlineData("pink")]
    [InlineData("red")]
    [InlineData("orange")]
    [InlineData("yellow")]
    public void Tag_AppliesColorVariant(string color)
    {
        // Arrange & Act
        var cut = Render<GovUkTag>(parameters => parameters
            .Add(p => p.Text, "Status")
            .Add(p => p.Color, color));

        // Assert
        var tag = cut.Find("strong.govuk-tag");
        Assert.Contains($"govuk-tag--{color}", tag.ClassName);
    }

    [Fact]
    public void Tag_NoColorClass_WhenColorNotProvided()
    {
        // Arrange & Act
        var cut = Render<GovUkTag>(parameters => parameters
            .Add(p => p.Text, "Active"));

        // Assert
        var tag = cut.Find("strong.govuk-tag");
        // Should only have govuk-tag class
        Assert.DoesNotContain("govuk-tag--", tag.ClassName);
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void Tag_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkTag>(parameters => parameters
            .Add(p => p.Text, "Active")
            .AddUnmatched("data-test-id", "test-tag")
            .AddUnmatched("id", "my-tag"));

        // Assert
        var tag = cut.Find("strong.govuk-tag");
        Assert.Equal("test-tag", tag.GetAttribute("data-test-id"));
        Assert.Equal("my-tag", tag.Id);
    }

    #endregion
}
