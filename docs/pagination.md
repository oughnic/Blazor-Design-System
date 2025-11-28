# Pagination Component

Use pagination to allow users to navigate between related pages of content.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkPagination` | The main pagination container |
| `GovUkPaginationItem` | Individual page number items |

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkPagination 
    PreviousHref="/results?page=1"
    NextHref="/results?page=3">
    <PageItems>
        <GovUkPaginationItem Number="1" Href="/results?page=1" />
        <GovUkPaginationItem Number="2" Href="/results?page=2" IsCurrent="true" />
        <GovUkPaginationItem Number="3" Href="/results?page=3" />
    </PageItems>
</GovUkPagination>
```

## GovUkPagination Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `AriaLabel` | `string` | `"Pagination"` | Aria label |
| `PreviousHref` | `string?` | `null` | Previous page URL |
| `PreviousText` | `string` | `"Previous"` | Previous link text |
| `PreviousLabel` | `string?` | `null` | Previous link description |
| `NextHref` | `string?` | `null` | Next page URL |
| `NextText` | `string` | `"Next"` | Next link text |
| `NextLabel` | `string?` | `null` | Next link description |
| `PageItems` | `RenderFragment?` | `null` | Page number items |
| `IsBlockLevel` | `bool` | `false` | Block level style |
| `CssClass` | `string?` | `null` | Additional CSS classes |

## GovUkPaginationItem Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Number` | `int` | `0` | Page number |
| `Href` | `string?` | `null` | Page URL |
| `IsCurrent` | `bool` | `false` | Is current page |
| `IsEllipsis` | `bool` | `false` | Render as ellipsis |

## Examples

### Simple Previous/Next

```razor
<GovUkPagination 
    PreviousHref="/page/1"
    NextHref="/page/3" />
```

### With Page Numbers

```razor
<GovUkPagination 
    PreviousHref="/results?page=2"
    NextHref="/results?page=4">
    <PageItems>
        <GovUkPaginationItem Number="1" Href="/results?page=1" />
        <GovUkPaginationItem Number="2" Href="/results?page=2" />
        <GovUkPaginationItem Number="3" Href="/results?page=3" IsCurrent="true" />
        <GovUkPaginationItem Number="4" Href="/results?page=4" />
        <GovUkPaginationItem Number="5" Href="/results?page=5" />
    </PageItems>
</GovUkPagination>
```

### With Ellipsis

```razor
<GovUkPagination 
    PreviousHref="/results?page=24"
    NextHref="/results?page=26">
    <PageItems>
        <GovUkPaginationItem Number="1" Href="/results?page=1" />
        <GovUkPaginationItem IsEllipsis="true" />
        <GovUkPaginationItem Number="24" Href="/results?page=24" />
        <GovUkPaginationItem Number="25" Href="/results?page=25" IsCurrent="true" />
        <GovUkPaginationItem Number="26" Href="/results?page=26" />
        <GovUkPaginationItem IsEllipsis="true" />
        <GovUkPaginationItem Number="50" Href="/results?page=50" />
    </PageItems>
</GovUkPagination>
```

### Block Level (For Content Pages)

```razor
<GovUkPagination 
    IsBlockLevel="true"
    PreviousHref="/chapter/1"
    PreviousLabel="Chapter 1: Introduction"
    NextHref="/chapter/3"
    NextLabel="Chapter 3: Advanced Topics" />
```

### First Page (No Previous)

```razor
<GovUkPagination NextHref="/results?page=2">
    <PageItems>
        <GovUkPaginationItem Number="1" IsCurrent="true" />
        <GovUkPaginationItem Number="2" Href="/results?page=2" />
        <GovUkPaginationItem Number="3" Href="/results?page=3" />
    </PageItems>
</GovUkPagination>
```

### Dynamic Pagination

```razor
@code {
    private int currentPage = 5;
    private int totalPages = 20;
    
    private string GetPageUrl(int page) => $"/results?page={page}";
    
    private IEnumerable<int> GetVisiblePages()
    {
        var pages = new List<int>();
        pages.Add(1);
        
        if (currentPage > 3) pages.Add(-1); // ellipsis
        
        for (int i = Math.Max(2, currentPage - 1); i <= Math.Min(totalPages - 1, currentPage + 1); i++)
        {
            pages.Add(i);
        }
        
        if (currentPage < totalPages - 2) pages.Add(-1); // ellipsis
        
        if (totalPages > 1) pages.Add(totalPages);
        
        return pages;
    }
}

<GovUkPagination 
    PreviousHref="@(currentPage > 1 ? GetPageUrl(currentPage - 1) : null)"
    NextHref="@(currentPage < totalPages ? GetPageUrl(currentPage + 1) : null)">
    <PageItems>
        @foreach (var page in GetVisiblePages())
        {
            @if (page == -1)
            {
                <GovUkPaginationItem IsEllipsis="true" />
            }
            else
            {
                <GovUkPaginationItem 
                    Number="@page" 
                    Href="@GetPageUrl(page)" 
                    IsCurrent="@(page == currentPage)" />
            }
        }
    </PageItems>
</GovUkPagination>
```

## Accessibility

- Uses `<nav>` element for navigation landmark
- `rel="prev"` and `rel="next"` on links
- Current page marked with `aria-current="page"`

## For AI Coding Agents

When implementing pagination:

1. Hide Previous link on first page
2. Hide Next link on last page
3. Use ellipsis for large page ranges
4. Always show first and last page
5. Use block level for content navigation

```razor
// Standard search results pagination
<GovUkPagination 
    PreviousHref="@prevUrl"
    NextHref="@nextUrl">
    <PageItems>
        <GovUkPaginationItem Number="1" Href="?page=1" IsCurrent="@(page == 1)" />
        @if (page > 3)
        {
            <GovUkPaginationItem IsEllipsis="true" />
        }
        @* Surrounding pages *@
        @if (page < totalPages - 2)
        {
            <GovUkPaginationItem IsEllipsis="true" />
        }
        <GovUkPaginationItem Number="@totalPages" Href="?page=@totalPages" IsCurrent="@(page == totalPages)" />
    </PageItems>
</GovUkPagination>
```
