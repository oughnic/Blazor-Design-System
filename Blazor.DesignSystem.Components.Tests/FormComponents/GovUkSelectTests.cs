using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.FormComponents;

/// <summary>
/// Unit tests for GovUkSelect component covering rendering, styling, accessibility, data binding, and error states.
/// </summary>
public class GovUkSelectTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Select_RendersWithFormGroupWrapper()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.NotNull(formGroup);
    }

    [Fact]
    public void Select_RendersLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Id, "select-id")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var label = cut.Find("label.govuk-label");
        Assert.Equal("Sort by", label.TextContent);
        Assert.Equal("select-id", label.GetAttribute("for"));
    }

    [Fact]
    public void Select_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Id, "select-id")
            .Add(p => p.Hint, "Choose how to sort the results")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var hint = cut.Find(".govuk-hint");
        Assert.Equal("Choose how to sort the results", hint.TextContent);
        Assert.Equal("select-id-hint", hint.Id);
    }

    [Fact]
    public void Select_RendersOptions()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .AddChildContent(@"
                <option value=""1"">Option 1</option>
                <option value=""2"">Option 2</option>
                <option value=""3"">Option 3</option>"));

        // Assert
        var options = cut.FindAll("select.govuk-select option");
        Assert.Equal(3, options.Count);
    }

    #endregion

    #region Data Binding Tests

    [Fact]
    public void Select_InvokesValueChanged_OnChange()
    {
        // Arrange
        string? newValue = null;
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, v => newValue = v))
            .AddChildContent(@"
                <option value=""1"">Option 1</option>
                <option value=""2"">Option 2</option>"));

        // Act
        cut.Find("select").Change("2");

        // Assert
        Assert.Equal("2", newValue);
    }

    [Fact]
    public void Select_TwoWayBinding_Works()
    {
        // Arrange
        var value = "1";
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, v => value = v))
            .AddChildContent(@"
                <option value=""1"">Option 1</option>
                <option value=""2"">Option 2</option>"));

        // Act
        cut.Find("select").Change("2");

        // Assert
        Assert.Equal("2", value);
    }

    #endregion

    #region Error State Tests

    [Fact]
    public void Select_ErrorState_AppliesErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select an option")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("govuk-form-group--error", formGroup.ClassName);
        
        var select = cut.Find("select.govuk-select");
        Assert.Contains("govuk-select--error", select.ClassName);
    }

    [Fact]
    public void Select_ErrorState_RendersErrorMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Id, "select-id")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select an option")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var errorMessage = cut.Find(".govuk-error-message");
        Assert.Contains("Select an option", errorMessage.TextContent);
        Assert.Equal("select-id-error", errorMessage.Id);
    }

    [Fact]
    public void Select_ErrorState_HasVisuallyHiddenPrefix()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select an option")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var visuallyHidden = cut.Find(".govuk-error-message .govuk-visually-hidden");
        Assert.Contains("Error", visuallyHidden.TextContent);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Select_HasAriaDescribedBy_WithHint()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Id, "select-id")
            .Add(p => p.Hint, "Choose an option")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var select = cut.Find("select.govuk-select");
        Assert.Equal("select-id-hint", select.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void Select_HasAriaDescribedBy_WithError()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Id, "select-id")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select an option")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var select = cut.Find("select.govuk-select");
        Assert.Equal("select-id-error", select.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void Select_HasAriaInvalid_WhenError()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select an option")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var select = cut.Find("select.govuk-select");
        Assert.Equal("true", select.GetAttribute("aria-invalid"));
    }

    [Fact]
    public void Select_LabelForAttribute_MatchesSelectId()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Id, "custom-id")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var label = cut.Find("label.govuk-label");
        var select = cut.Find("select.govuk-select");
        Assert.Equal(select.Id, label.GetAttribute("for"));
        Assert.Equal("custom-id", select.Id);
    }

    #endregion

    #region Attributes Tests

    [Fact]
    public void Select_AppliesDisabled()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Disabled, true)
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var select = cut.Find("select.govuk-select");
        Assert.True(select.HasAttribute("disabled"));
    }

    [Fact]
    public void Select_AppliesName()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.Name, "sortBy")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var select = cut.Find("select.govuk-select");
        Assert.Equal("sortBy", select.GetAttribute("name"));
    }

    [Fact]
    public void Select_AppliesCustomCssClasses()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .Add(p => p.CssClass, "form-group-custom")
            .Add(p => p.SelectCssClass, "select-custom")
            .Add(p => p.LabelCssClass, "label-custom")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("form-group-custom", formGroup.ClassName);
        
        var select = cut.Find("select.govuk-select");
        Assert.Contains("select-custom", select.ClassName);
        
        var label = cut.Find("label.govuk-label");
        Assert.Contains("label-custom", label.ClassName);
    }

    [Fact]
    public void Select_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkSelect>(parameters => parameters
            .Add(p => p.Label, "Sort by")
            .AddUnmatched("data-test-id", "test-select")
            .AddChildContent("<option value=\"1\">Option 1</option>"));

        // Assert
        var select = cut.Find("select.govuk-select");
        Assert.Equal("test-select", select.GetAttribute("data-test-id"));
    }

    #endregion
}
