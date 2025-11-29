using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.FormComponents;

/// <summary>
/// Unit tests for GovUkTextarea component covering rendering, styling, accessibility, data binding, and error states.
/// </summary>
public class GovUkTextareaTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Textarea_RendersWithFormGroupWrapper()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.NotNull(formGroup);
    }

    [Fact]
    public void Textarea_RendersLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "textarea-id"));

        // Assert
        var label = cut.Find("label.govuk-label");
        Assert.Equal("Description", label.TextContent);
        Assert.Equal("textarea-id", label.GetAttribute("for"));
    }

    [Fact]
    public void Textarea_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "textarea-id")
            .Add(p => p.Hint, "Enter a detailed description"));

        // Assert
        var hint = cut.Find(".govuk-hint");
        Assert.Equal("Enter a detailed description", hint.TextContent);
        Assert.Equal("textarea-id-hint", hint.Id);
    }

    [Fact]
    public void Textarea_RendersWithDefaultRows()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("5", textarea.GetAttribute("rows"));
    }

    [Fact]
    public void Textarea_RendersWithCustomRows()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Rows, 10));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("10", textarea.GetAttribute("rows"));
    }

    #endregion

    #region Data Binding Tests

    [Fact]
    public void Textarea_DisplaysValue()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Value, "Initial text content"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("Initial text content", textarea.TextContent);
    }

    [Fact]
    public void Textarea_InvokesValueChanged_OnInput()
    {
        // Arrange
        string? newValue = null;
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, v => newValue = v)));

        // Act
        cut.Find("textarea").Input("New content");

        // Assert
        Assert.Equal("New content", newValue);
    }

    [Fact]
    public void Textarea_TwoWayBinding_Works()
    {
        // Arrange
        var value = "Initial";
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, v => value = v)));

        // Act
        cut.Find("textarea").Input("Updated content");

        // Assert
        Assert.Equal("Updated content", value);
    }

    #endregion

    #region Error State Tests

    [Fact]
    public void Textarea_ErrorState_AppliesErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter a description"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("govuk-form-group--error", formGroup.ClassName);
        
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Contains("govuk-textarea--error", textarea.ClassName);
    }

    [Fact]
    public void Textarea_ErrorState_RendersErrorMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "textarea-id")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter a description"));

        // Assert
        var errorMessage = cut.Find(".govuk-error-message");
        Assert.Contains("Enter a description", errorMessage.TextContent);
        Assert.Equal("textarea-id-error", errorMessage.Id);
    }

    [Fact]
    public void Textarea_ErrorState_HasVisuallyHiddenPrefix()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter a description"));

        // Assert
        var visuallyHidden = cut.Find(".govuk-error-message .govuk-visually-hidden");
        Assert.Contains("Error", visuallyHidden.TextContent);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Textarea_HasAriaDescribedBy_WithHint()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "textarea-id")
            .Add(p => p.Hint, "Enter details"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("textarea-id-hint", textarea.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void Textarea_HasAriaDescribedBy_WithError()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "textarea-id")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter a description"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("textarea-id-error", textarea.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void Textarea_HasAriaInvalid_WhenError()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter a description"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("true", textarea.GetAttribute("aria-invalid"));
    }

    [Fact]
    public void Textarea_LabelForAttribute_MatchesTextareaId()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "custom-id"));

        // Assert
        var label = cut.Find("label.govuk-label");
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal(textarea.Id, label.GetAttribute("for"));
        Assert.Equal("custom-id", textarea.Id);
    }

    #endregion

    #region Attributes Tests

    [Fact]
    public void Textarea_AppliesAutocomplete()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Autocomplete, "street-address"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("street-address", textarea.GetAttribute("autocomplete"));
    }

    [Fact]
    public void Textarea_AppliesSpellcheck()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Spellcheck, true));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("true", textarea.GetAttribute("spellcheck"));
    }

    [Fact]
    public void Textarea_AppliesDisabled()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Disabled, true));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.True(textarea.HasAttribute("disabled"));
    }

    [Fact]
    public void Textarea_AppliesName()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Name, "description"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("description", textarea.GetAttribute("name"));
    }

    [Fact]
    public void Textarea_AppliesCustomCssClasses()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.CssClass, "form-group-custom")
            .Add(p => p.TextareaCssClass, "textarea-custom")
            .Add(p => p.LabelCssClass, "label-custom"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("form-group-custom", formGroup.ClassName);
        
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Contains("textarea-custom", textarea.ClassName);
        
        var label = cut.Find("label.govuk-label");
        Assert.Contains("label-custom", label.ClassName);
    }

    [Fact]
    public void Textarea_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkTextarea>(parameters => parameters
            .Add(p => p.Label, "Description")
            .AddUnmatched("data-test-id", "test-textarea")
            .AddUnmatched("maxlength", "500"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("test-textarea", textarea.GetAttribute("data-test-id"));
        Assert.Equal("500", textarea.GetAttribute("maxlength"));
    }

    #endregion
}
