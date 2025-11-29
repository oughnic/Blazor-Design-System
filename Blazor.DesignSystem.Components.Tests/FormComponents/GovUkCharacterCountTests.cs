using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.FormComponents;

/// <summary>
/// Unit tests for GovUkCharacterCount component covering rendering, counting, accessibility, and error states.
/// </summary>
public class GovUkCharacterCountTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void CharacterCount_RendersWithDataModule()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxLength, 200));

        // Assert
        var component = cut.Find(".govuk-character-count");
        Assert.Equal("govuk-character-count", component.GetAttribute("data-module"));
    }

    [Fact]
    public void CharacterCount_RendersLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200));

        // Assert
        var label = cut.Find("label.govuk-label");
        Assert.Equal("Description", label.TextContent);
        Assert.Equal("cc-id", label.GetAttribute("for"));
    }

    [Fact]
    public void CharacterCount_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.Hint, "Enter a brief description")
            .Add(p => p.MaxLength, 200));

        // Assert
        var hint = cut.Find(".govuk-hint");
        Assert.Equal("Enter a brief description", hint.TextContent);
        Assert.Equal("cc-id-hint", hint.Id);
    }

    [Fact]
    public void CharacterCount_RendersTextarea()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxLength, 200));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.NotNull(textarea);
        Assert.Contains("govuk-js-character-count", textarea.ClassName);
    }

    [Fact]
    public void CharacterCount_RendersCountMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.NotNull(countMessage);
        Assert.Contains("200 characters remaining", countMessage.TextContent);
    }

    #endregion

    #region Character Counting Tests

    [Fact]
    public void CharacterCount_DisplaysRemainingCharacters()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200)
            .Add(p => p.Value, "Hello"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("195 characters remaining", countMessage.TextContent);
    }

    [Fact]
    public void CharacterCount_SingleCharacterRemaining()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 10)
            .Add(p => p.Value, "123456789"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("1 character remaining", countMessage.TextContent);
    }

    [Fact]
    public void CharacterCount_AtLimit()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 10)
            .Add(p => p.Value, "1234567890"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("0 characters remaining", countMessage.TextContent);
    }

    [Fact]
    public void CharacterCount_OverLimit()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 10)
            .Add(p => p.Value, "12345678901234"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("4 characters too many", countMessage.TextContent);
    }

    [Fact]
    public void CharacterCount_SingleCharacterOverLimit()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 10)
            .Add(p => p.Value, "12345678901"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("1 character too many", countMessage.TextContent);
    }

    #endregion

    #region Word Counting Tests

    [Fact]
    public void CharacterCount_CountsWords_WhenMaxWordsSet()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxWords, 100));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("100 words remaining", countMessage.TextContent);
    }

    [Fact]
    public void CharacterCount_DisplaysRemainingWords()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxWords, 100)
            .Add(p => p.Value, "one two three four five"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("95 words remaining", countMessage.TextContent);
    }

    [Fact]
    public void CharacterCount_SingleWordRemaining()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxWords, 5)
            .Add(p => p.Value, "one two three four"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("1 word remaining", countMessage.TextContent);
    }

    [Fact]
    public void CharacterCount_WordsOverLimit()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxWords, 3)
            .Add(p => p.Value, "one two three four five"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("2 words too many", countMessage.TextContent);
    }

    #endregion

    #region Error State Tests

    [Fact]
    public void CharacterCount_OverLimit_AppliesErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxLength, 10)
            .Add(p => p.Value, "12345678901234"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("govuk-form-group--error", formGroup.ClassName);
        
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Contains("govuk-textarea--error", textarea.ClassName);
    }

    [Fact]
    public void CharacterCount_HasError_AppliesErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxLength, 200)
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter a description"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("govuk-form-group--error", formGroup.ClassName);
    }

    [Fact]
    public void CharacterCount_HasError_RendersErrorMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200)
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter a description"));

        // Assert
        var errorMessage = cut.Find(".govuk-error-message");
        Assert.Contains("Enter a description", errorMessage.TextContent);
        Assert.Equal("cc-id-error", errorMessage.Id);
    }

    [Fact]
    public void CharacterCount_OverLimit_CountMessageHasErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 10)
            .Add(p => p.Value, "12345678901234"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("govuk-error-message", countMessage.ClassName);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void CharacterCount_HasAriaDescribedBy()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        var ariaDescribedBy = textarea.GetAttribute("aria-describedby");
        Assert.Contains("cc-id-info", ariaDescribedBy);
    }

    [Fact]
    public void CharacterCount_HasAriaDescribedBy_WithHintAndError()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.Hint, "Enter details")
            .Add(p => p.MaxLength, 200)
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "This field is required"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        var ariaDescribedBy = textarea.GetAttribute("aria-describedby");
        Assert.Contains("cc-id-hint", ariaDescribedBy);
        Assert.Contains("cc-id-error", ariaDescribedBy);
        Assert.Contains("cc-id-info", ariaDescribedBy);
    }

    [Fact]
    public void CharacterCount_HasAriaInvalid_WhenOverLimit()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxLength, 10)
            .Add(p => p.Value, "12345678901234"));

        // Assert
        var textarea = cut.Find("textarea.govuk-textarea");
        Assert.Equal("true", textarea.GetAttribute("aria-invalid"));
    }

    [Fact]
    public void CharacterCount_ScreenReaderStatus_HasAriaLive()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200));

        // Assert
        var srStatus = cut.Find("#cc-id-sr-status");
        Assert.Equal("polite", srStatus.GetAttribute("aria-live"));
    }

    [Fact]
    public void CharacterCount_VisibleStatus_HasAriaHidden()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200));

        // Assert
        var visibleStatus = cut.Find("#cc-id-info");
        Assert.Equal("true", visibleStatus.GetAttribute("aria-hidden"));
    }

    #endregion

    #region Threshold Tests

    [Fact]
    public void CharacterCount_Threshold_HidesMessageBelowThreshold()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 100)
            .Add(p => p.Threshold, 75)
            .Add(p => p.Value, "test")); // 4 chars out of 100, well below 75%

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("govuk-character-count__message--disabled", countMessage.ClassName);
    }

    [Fact]
    public void CharacterCount_Threshold_ShowsMessageAboveThreshold()
    {
        // Build 76 character string (above 75% threshold of 100)
        var value = new string('a', 76);
        
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 100)
            .Add(p => p.Threshold, 75)
            .Add(p => p.Value, value));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.DoesNotContain("govuk-character-count__message--disabled", countMessage.ClassName);
    }

    #endregion

    #region Data Attributes Tests

    [Fact]
    public void CharacterCount_HasMaxLengthDataAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxLength, 200));

        // Assert
        var component = cut.Find(".govuk-character-count");
        Assert.Equal("200", component.GetAttribute("data-maxlength"));
    }

    [Fact]
    public void CharacterCount_HasMaxWordsDataAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxWords, 100));

        // Assert
        var component = cut.Find(".govuk-character-count");
        Assert.Equal("100", component.GetAttribute("data-maxwords"));
        Assert.Null(component.GetAttribute("data-maxlength"));
    }

    [Fact]
    public void CharacterCount_HasThresholdDataAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxLength, 200)
            .Add(p => p.Threshold, 75));

        // Assert
        var component = cut.Find(".govuk-character-count");
        Assert.Equal("75", component.GetAttribute("data-threshold"));
    }

    #endregion

    #region Data Binding Tests

    [Fact]
    public void CharacterCount_InvokesValueChanged_OnInput()
    {
        // Arrange
        string? newValue = null;
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.MaxLength, 200)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string?>(this, v => newValue = v)));

        // Act
        cut.Find("textarea").Input("New content");

        // Assert
        Assert.Equal("New content", newValue);
    }

    [Fact]
    public void CharacterCount_UpdatesCount_OnInput()
    {
        // Arrange
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200));

        // Act
        cut.Find("textarea").Input("Hello World");

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("189 characters remaining", countMessage.TextContent);
    }

    #endregion

    #region Custom Message Tests

    [Fact]
    public void CharacterCount_CustomUnderLimitMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 200)
            .Add(p => p.CharactersUnderLimitText, "Mae gennych {count} nod ar ôl"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("Mae gennych 200 nod ar ôl", countMessage.TextContent);
    }

    [Fact]
    public void CharacterCount_CustomOverLimitMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkCharacterCount>(parameters => parameters
            .Add(p => p.Label, "Description")
            .Add(p => p.Id, "cc-id")
            .Add(p => p.MaxLength, 10)
            .Add(p => p.Value, "12345678901234")
            .Add(p => p.CharactersOverLimitText, "You have exceeded by {count} characters"));

        // Assert
        var countMessage = cut.Find("#cc-id-info");
        Assert.Contains("You have exceeded by 4 characters", countMessage.TextContent);
    }

    #endregion
}
