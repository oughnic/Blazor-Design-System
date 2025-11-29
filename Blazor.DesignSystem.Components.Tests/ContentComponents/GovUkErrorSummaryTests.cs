using Bunit;
using Bunit.TestDoubles;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.ContentComponents;

/// <summary>
/// Unit tests for GovUkErrorSummary component covering rendering and accessibility.
/// </summary>
public class GovUkErrorSummaryTests : BunitContext
{
    public GovUkErrorSummaryTests()
    {
        // Add fake IJSRuntime for the component that uses JS interop
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    #region Basic Rendering Tests

    [Fact]
    public void ErrorSummary_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>();

        // Assert
        var summary = cut.Find(".govuk-error-summary");
        Assert.NotNull(summary);
    }

    [Fact]
    public void ErrorSummary_HasDataModuleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>();

        // Assert
        var summary = cut.Find(".govuk-error-summary");
        Assert.Equal("govuk-error-summary", summary.GetAttribute("data-module"));
    }

    [Fact]
    public void ErrorSummary_RendersDefaultTitle()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>();

        // Assert
        var title = cut.Find(".govuk-error-summary__title");
        Assert.Equal("There is a problem", title.TextContent);
    }

    [Fact]
    public void ErrorSummary_RendersCustomTitle()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>(parameters => parameters
            .Add(p => p.Title, "Please fix the following errors"));

        // Assert
        var title = cut.Find(".govuk-error-summary__title");
        Assert.Equal("Please fix the following errors", title.TextContent);
    }

    [Fact]
    public void ErrorSummary_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>(parameters => parameters
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var summary = cut.Find(".govuk-error-summary");
        Assert.Contains("custom-class", summary.ClassName);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void ErrorSummary_HasRoleAlert()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>();

        // Assert
        var alert = cut.Find("[role='alert']");
        Assert.NotNull(alert);
    }

    [Fact]
    public void ErrorSummary_HasTabIndexMinusOne()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>();

        // Assert
        var summary = cut.Find(".govuk-error-summary");
        Assert.Equal("-1", summary.GetAttribute("tabindex"));
    }

    #endregion

    #region Error List Tests

    [Fact]
    public void ErrorSummary_RendersErrorList()
    {
        // Arrange
        var errors = new[]
        {
            new GovUkErrorSummary.ErrorItem { Message = "Enter your full name", FieldId = "name" },
            new GovUkErrorSummary.ErrorItem { Message = "Enter a valid email address", FieldId = "email" }
        };

        // Act
        var cut = Render<GovUkErrorSummary>(parameters => parameters
            .Add(p => p.Errors, errors));

        // Assert
        var errorList = cut.Find(".govuk-error-summary__list");
        Assert.NotNull(errorList);
        
        var items = cut.FindAll(".govuk-error-summary__list li");
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void ErrorSummary_ErrorsHaveLinks_WhenFieldIdProvided()
    {
        // Arrange
        var errors = new[]
        {
            new GovUkErrorSummary.ErrorItem { Message = "Enter your full name", FieldId = "name" }
        };

        // Act
        var cut = Render<GovUkErrorSummary>(parameters => parameters
            .Add(p => p.Errors, errors));

        // Assert
        var link = cut.Find(".govuk-error-summary__list a");
        Assert.Equal("#name", link.GetAttribute("href"));
        Assert.Contains("Enter your full name", link.TextContent);
    }

    [Fact]
    public void ErrorSummary_ErrorsNoLink_WhenNoFieldId()
    {
        // Arrange
        var errors = new[]
        {
            new GovUkErrorSummary.ErrorItem { Message = "A general error occurred" }
        };

        // Act
        var cut = Render<GovUkErrorSummary>(parameters => parameters
            .Add(p => p.Errors, errors));

        // Assert
        var listItem = cut.Find(".govuk-error-summary__list li");
        Assert.Contains("A general error occurred", listItem.TextContent);
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find(".govuk-error-summary__list a"));
    }

    #endregion

    #region Description Tests

    [Fact]
    public void ErrorSummary_RendersDescription()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>(parameters => parameters
            .Add(p => p.Description, "Please correct the errors below."));

        // Assert
        var body = cut.Find(".govuk-error-summary__body");
        Assert.Contains("Please correct the errors below.", body.TextContent);
    }

    #endregion

    #region Auto Focus Tests

    [Fact]
    public void ErrorSummary_DisableAutoFocusAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>(parameters => parameters
            .Add(p => p.DisableAutoFocus, true));

        // Assert
        var summary = cut.Find(".govuk-error-summary");
        Assert.Equal("true", summary.GetAttribute("data-disable-auto-focus"));
    }

    [Fact]
    public void ErrorSummary_AutoFocusEnabled_ByDefault()
    {
        // Arrange & Act
        var cut = Render<GovUkErrorSummary>();

        // Assert
        var summary = cut.Find(".govuk-error-summary");
        Assert.Null(summary.GetAttribute("data-disable-auto-focus"));
    }

    #endregion
}
