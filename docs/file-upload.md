# File Upload Component

Use the file upload component to let users select and upload a file.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkFileUpload 
    Id="document"
    Label="Upload a document"
    OnChange="HandleFileChange" />

@code {
    private void HandleFileChange(ChangeEventArgs e)
    {
        // Handle file selection
    }
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string` | Auto-generated GUID | Unique identifier |
| `Name` | `string?` | `null` | Name attribute for form submission |
| `Label` | `string?` | `null` | Label text |
| `LabelCssClass` | `string?` | `null` | Additional CSS for label |
| `Hint` | `string?` | `null` | Hint text |
| `Disabled` | `bool` | `false` | Whether upload is disabled |
| `HasError` | `bool` | `false` | Whether field has error |
| `ErrorMessage` | `string?` | `null` | Error message |
| `Accept` | `string?` | `null` | Accepted file types |
| `Multiple` | `bool` | `false` | Allow multiple files |
| `OnChange` | `EventCallback<ChangeEventArgs>` | - | File change callback |
| `CssClass` | `string?` | `null` | Additional CSS for form group |
| `InputCssClass` | `string?` | `null` | Additional CSS for input |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic File Upload

```razor
<GovUkFileUpload 
    Id="cv"
    Label="Upload your CV"
    Hint="Your CV should be a PDF or Word document"
    OnChange="HandleFile" />
```

### With File Type Restrictions

```razor
<GovUkFileUpload 
    Id="photo"
    Label="Upload a photo"
    Hint="The photo must be a JPG, PNG or GIF file"
    Accept=".jpg,.jpeg,.png,.gif"
    OnChange="HandlePhoto" />
```

### With PDF Only

```razor
<GovUkFileUpload 
    Id="document"
    Label="Upload supporting document"
    Hint="Must be a PDF file, maximum 10MB"
    Accept="application/pdf,.pdf"
    OnChange="HandleDocument" />
```

### Multiple Files

```razor
<GovUkFileUpload 
    Id="attachments"
    Label="Upload attachments"
    Hint="You can upload multiple files"
    Multiple="true"
    OnChange="HandleMultipleFiles" />
```

### With Error State

```razor
<GovUkFileUpload 
    Id="evidence"
    Label="Upload evidence"
    HasError="true"
    ErrorMessage="Select a file to upload"
    OnChange="HandleFile" />
```

### Complete Example with Blazor File Handling

```razor
@rendermode InteractiveServer
@using Microsoft.AspNetCore.Components.Forms

<GovUkFileUpload 
    Id="document"
    Label="Upload a document"
    Hint="PDF, DOC or DOCX files only, max 5MB"
    Accept=".pdf,.doc,.docx"
    HasError="@hasError"
    ErrorMessage="@errorMessage"
    OnChange="HandleFileSelected" />

@if (selectedFileName != null)
{
    <p class="govuk-body">Selected: @selectedFileName</p>
}

@code {
    private string? selectedFileName;
    private bool hasError = false;
    private string? errorMessage;
    
    private void HandleFileSelected(ChangeEventArgs e)
    {
        hasError = false;
        errorMessage = null;
        
        // Note: For full file handling in Blazor, you may need to use
        // InputFile component or JavaScript interop
        selectedFileName = "File selected";
    }
}
```

### Using InputFile for Full Blazor Integration

For complete file upload functionality, consider using Blazor's `InputFile` with GOV.UK styling:

```razor
@rendermode InteractiveServer

<div class="govuk-form-group">
    <label class="govuk-label" for="file-upload">
        Upload a file
    </label>
    <div class="govuk-hint">
        Maximum file size: 10MB
    </div>
    <InputFile 
        id="file-upload"
        class="govuk-file-upload"
        OnChange="HandleInputFile"
        accept=".pdf,.doc,.docx" />
</div>

@if (uploadedFile != null)
{
    <p class="govuk-body">
        File: @uploadedFile.Name (@uploadedFile.Size bytes)
    </p>
}

@code {
    private IBrowserFile? uploadedFile;
    
    private void HandleInputFile(InputFileChangeEventArgs e)
    {
        uploadedFile = e.File;
    }
}
```

## Accessibility

- Uses proper label association via `for` attribute
- Error states include visually hidden "Error:" prefix
- Hint text linked via `aria-describedby`
- Native file input is keyboard accessible

## For AI Coding Agents

When implementing file uploads:

1. Always specify accepted file types with `Accept`
2. Include hint text about allowed formats and size limits
3. For Blazor file processing, consider using `InputFile` component
4. Validate file type and size on both client and server
5. Provide clear error messages for validation failures

```razor
@rendermode InteractiveServer

// Basic file upload pattern
<GovUkFileUpload 
    Id="unique-id"
    Label="Upload your document"
    Hint="PDF or Word document, max 10MB"
    Accept=".pdf,.doc,.docx,application/pdf"
    HasError="@hasError"
    ErrorMessage="@errorMessage"
    OnChange="HandleFile" />

@code {
    private bool hasError = false;
    private string? errorMessage;
    
    private void HandleFile(ChangeEventArgs e)
    {
        // Validate and process
    }
}

// For full Blazor file handling, use InputFile with GOV.UK classes
<div class="govuk-form-group @(hasError ? "govuk-form-group--error" : "")">
    <label class="govuk-label" for="upload">Label</label>
    <div class="govuk-hint">Hint text</div>
    @if (hasError)
    {
        <p class="govuk-error-message">
            <span class="govuk-visually-hidden">Error:</span> @errorMessage
        </p>
    }
    <InputFile 
        id="upload"
        class="govuk-file-upload @(hasError ? "govuk-file-upload--error" : "")"
        OnChange="HandleFiles" />
</div>

@code {
    private async Task HandleFiles(InputFileChangeEventArgs e)
    {
        var file = e.File;
        
        // Validate size
        if (file.Size > 10 * 1024 * 1024)
        {
            hasError = true;
            errorMessage = "File must be smaller than 10MB";
            return;
        }
        
        // Process file
        using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
        // ... handle upload
    }
}
```

Common accept values:
- Images: `image/*` or `.jpg,.jpeg,.png,.gif`
- Documents: `.pdf,.doc,.docx`
- Spreadsheets: `.xlsx,.xls,.csv`
- All files: (leave empty)
