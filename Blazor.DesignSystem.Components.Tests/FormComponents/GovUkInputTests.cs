using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.FormComponents;

/// <summary>
/// Unit tests for GovUkInput component covering rendering, styling, accessibility, data binding, and error states.
/// </summary>
public class GovUkInputTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Input_RendersWithFormGroupWrapper()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.NotNull(formGroup);
    }

    [Fact]
    public void Input_RendersLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Id, "input-name"));

        // Assert
        var label = cut.Find("label.govuk-label");
        Assert.Equal("Full name", label.TextContent);
        Assert.Equal("input-name", label.GetAttribute("for"));
    }

    [Fact]
    public void Input_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Id, "input-name")
            .Add(p => p.Hint, "Enter your full name as shown on your passport"));

        // Assert
        var hint = cut.Find(".govuk-hint");
        Assert.Equal("Enter your full name as shown on your passport", hint.TextContent);
        Assert.Equal("input-name-hint", hint.Id);
    }

    [Fact]
    public void Input_RendersWithDefaultTextType()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("text", input.GetAttribute("type"));
    }

    [Fact]
    public void Input_RendersWithCustomType()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Email")
            .Add(p => p.Type, "email"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("email", input.GetAttribute("type"));
    }

    #endregion

    #region Data Binding Tests

    [Fact]
    public void Input_DisplaysValue()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Value, "John Smith"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("John Smith", input.GetAttribute("value"));
    }

    [Fact]
    public void Input_InvokesValueChanged_OnInput()
    {
        // Arrange
        string? newValue = null;
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, v => newValue = v)));

        // Act
        cut.Find("input").Input("New Value");

        // Assert
        Assert.Equal("New Value", newValue);
    }

    [Fact]
    public void Input_TwoWayBinding_Works()
    {
        // Arrange
        var value = "Initial";
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, v => value = v)));

        // Act
        cut.Find("input").Input("Updated");

        // Assert
        Assert.Equal("Updated", value);
    }

    #endregion

    #region Error State Tests

    [Fact]
    public void Input_ErrorState_AppliesErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your full name"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("govuk-form-group--error", formGroup.ClassName);
        
        var input = cut.Find("input.govuk-input");
        Assert.Contains("govuk-input--error", input.ClassName);
    }

    [Fact]
    public void Input_ErrorState_RendersErrorMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Id, "input-name")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your full name"));

        // Assert
        var errorMessage = cut.Find(".govuk-error-message");
        Assert.Contains("Enter your full name", errorMessage.TextContent);
        Assert.Equal("input-name-error", errorMessage.Id);
    }

    [Fact]
    public void Input_ErrorState_HasVisuallyHiddenPrefix()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your full name"));

        // Assert
        var visuallyHidden = cut.Find(".govuk-error-message .govuk-visually-hidden");
        Assert.Contains("Error", visuallyHidden.TextContent);
    }

    [Fact]
    public void Input_ErrorState_CustomVisuallyHiddenText()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your full name")
            .Add(p => p.ErrorMessageVisuallyHiddenText, "Gwall"));

        // Assert
        var visuallyHidden = cut.Find(".govuk-error-message .govuk-visually-hidden");
        Assert.Contains("Gwall", visuallyHidden.TextContent);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Input_HasAriaDescribedBy_WithHint()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Id, "input-name")
            .Add(p => p.Hint, "Enter your full name"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("input-name-hint", input.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void Input_HasAriaDescribedBy_WithError()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Id, "input-name")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your full name"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("input-name-error", input.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void Input_HasAriaDescribedBy_WithHintAndError()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Id, "input-name")
            .Add(p => p.Hint, "Enter your full name")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "This field is required"));

        // Assert
        var input = cut.Find("input.govuk-input");
        var ariaDescribedBy = input.GetAttribute("aria-describedby");
        Assert.Contains("input-name-hint", ariaDescribedBy);
        Assert.Contains("input-name-error", ariaDescribedBy);
    }

    [Fact]
    public void Input_HasAriaInvalid_WhenError()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your full name"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("true", input.GetAttribute("aria-invalid"));
    }

    [Fact]
    public void Input_DoesNotHaveAriaInvalid_WhenNoError()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Null(input.GetAttribute("aria-invalid"));
    }

    [Fact]
    public void Input_LabelForAttribute_MatchesInputId()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Id, "custom-id"));

        // Assert
        var label = cut.Find("label.govuk-label");
        var input = cut.Find("input.govuk-input");
        Assert.Equal(input.Id, label.GetAttribute("for"));
        Assert.Equal("custom-id", input.Id);
    }

    #endregion

    #region Width Tests

    [Fact]
    public void Input_AppliesCharacterWidth()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Sort code")
            .Add(p => p.Width, "5"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Contains("govuk-input--width-5", input.ClassName);
    }

    [Fact]
    public void Input_AppliesFluidWidth()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Address")
            .Add(p => p.Width, "full"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Contains("govuk-!-width-full", input.ClassName);
    }

    [Theory]
    [InlineData("20")]
    [InlineData("10")]
    [InlineData("5")]
    [InlineData("4")]
    [InlineData("3")]
    [InlineData("2")]
    public void Input_AppliesCharacterWidthClasses(string width)
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Test")
            .Add(p => p.Width, width));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Contains($"govuk-input--width-{width}", input.ClassName);
    }

    #endregion

    #region Attributes Tests

    [Fact]
    public void Input_AppliesAutocomplete()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Email")
            .Add(p => p.Autocomplete, "email"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("email", input.GetAttribute("autocomplete"));
    }

    [Fact]
    public void Input_AppliesInputMode()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Phone")
            .Add(p => p.InputMode, "numeric"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("numeric", input.GetAttribute("inputmode"));
    }

    [Fact]
    public void Input_AppliesPattern()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Number")
            .Add(p => p.Pattern, "[0-9]*"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("[0-9]*", input.GetAttribute("pattern"));
    }

    [Fact]
    public void Input_AppliesSpellcheck()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Spellcheck, false));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("false", input.GetAttribute("spellcheck"));
    }

    [Fact]
    public void Input_AppliesDisabled()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Disabled, true));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.True(input.HasAttribute("disabled"));
    }

    [Fact]
    public void Input_AppliesName()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.Name, "fullName"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("fullName", input.GetAttribute("name"));
    }

    [Fact]
    public void Input_AppliesCustomCssClasses()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .Add(p => p.CssClass, "form-group-custom")
            .Add(p => p.InputCssClass, "input-custom")
            .Add(p => p.LabelCssClass, "label-custom"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("form-group-custom", formGroup.ClassName);
        
        var input = cut.Find("input.govuk-input");
        Assert.Contains("input-custom", input.ClassName);
        
        var label = cut.Find("label.govuk-label");
        Assert.Contains("label-custom", label.ClassName);
    }

    [Fact]
    public void Input_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkInput>(parameters => parameters
            .Add(p => p.Label, "Full name")
            .AddUnmatched("data-test-id", "test-input")
            .AddUnmatched("maxlength", "50"));

        // Assert
        var input = cut.Find("input.govuk-input");
        Assert.Equal("test-input", input.GetAttribute("data-test-id"));
        Assert.Equal("50", input.GetAttribute("maxlength"));
    }

    #endregion
}
