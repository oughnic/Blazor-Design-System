# Footer Component

The footer provides copyright, licensing and other information about your service.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkFooter` | The main footer container |
| `GovUkFooterMetaLink` | Links in the footer meta section |

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkFooter 
    LicenceText="All content is available under the <a class='govuk-footer__link' href='https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/'>Open Government Licence v3.0</a>, except where otherwise stated"
    CopyrightText="© Crown copyright"
    CopyrightUrl="https://www.nationalarchives.gov.uk/information-management/re-using-public-sector-information/uk-government-licensing-framework/crown-copyright/" />
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Navigation` | `RenderFragment?` | `null` | Navigation sections content |
| `MetaLinks` | `RenderFragment?` | `null` | Meta links in footer |
| `CustomContent` | `RenderFragment?` | `null` | Custom content in meta area |
| `LicenceText` | `string?` | `null` | Licence text (can contain HTML) |
| `CopyrightText` | `string?` | `null` | Copyright text |
| `CopyrightUrl` | `string?` | `null` | URL for copyright link |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## GovUkFooterMetaLink Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Href` | `string?` | `null` | The URL for the link |
| `Text` | `string` | **Required** | The link text |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Footer

```razor
<GovUkFooter 
    LicenceText="Content licensed under Open Government Licence v3.0"
    CopyrightText="© Crown copyright" />
```

### With Meta Links

```razor
<GovUkFooter 
    LicenceText="All content is available under the Open Government Licence v3.0">
    <MetaLinks>
        <GovUkFooterMetaLink Href="/help" Text="Help" />
        <GovUkFooterMetaLink Href="/cookies" Text="Cookies" />
        <GovUkFooterMetaLink Href="/contact" Text="Contact" />
        <GovUkFooterMetaLink Href="/terms" Text="Terms and conditions" />
    </MetaLinks>
</GovUkFooter>
```

### With Custom Content

```razor
<GovUkFooter>
    <CustomContent>
        <p class="govuk-footer__meta-custom">
            Built by the <a class="govuk-footer__link" href="#">Example Department</a>
        </p>
    </CustomContent>
</GovUkFooter>
```

### With Navigation Sections

```razor
<GovUkFooter 
    LicenceText="Content available under Open Government Licence v3.0">
    <Navigation>
        <div class="govuk-footer__section">
            <h2 class="govuk-footer__heading govuk-heading-m">Services</h2>
            <ul class="govuk-footer__list">
                <li class="govuk-footer__list-item">
                    <a class="govuk-footer__link" href="/service-1">Service 1</a>
                </li>
                <li class="govuk-footer__list-item">
                    <a class="govuk-footer__link" href="/service-2">Service 2</a>
                </li>
            </ul>
        </div>
        <div class="govuk-footer__section">
            <h2 class="govuk-footer__heading govuk-heading-m">Support</h2>
            <ul class="govuk-footer__list">
                <li class="govuk-footer__list-item">
                    <a class="govuk-footer__link" href="/help">Help</a>
                </li>
                <li class="govuk-footer__list-item">
                    <a class="govuk-footer__link" href="/contact">Contact</a>
                </li>
            </ul>
        </div>
    </Navigation>
    <MetaLinks>
        <GovUkFooterMetaLink Href="/privacy" Text="Privacy" />
        <GovUkFooterMetaLink Href="/accessibility" Text="Accessibility" />
    </MetaLinks>
</GovUkFooter>
```

### Complete Page Layout

```razor
<GovUkSkipLink />
<GovUkHeader OrganisationName="Service Name" />

<div class="govuk-width-container">
    <main class="govuk-main-wrapper" id="main-content">
        @* Page content *@
    </main>
</div>

<GovUkFooter 
    LicenceText="All content is available under the <a class='govuk-footer__link' href='https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/'>Open Government Licence v3.0</a>"
    CopyrightText="© Crown copyright"
    CopyrightUrl="https://www.nationalarchives.gov.uk/information-management/re-using-public-sector-information/uk-government-licensing-framework/crown-copyright/">
    <MetaLinks>
        <GovUkFooterMetaLink Href="/help" Text="Help" />
        <GovUkFooterMetaLink Href="/cookies" Text="Cookies" />
        <GovUkFooterMetaLink Href="/accessibility" Text="Accessibility statement" />
        <GovUkFooterMetaLink Href="/contact" Text="Contact" />
        <GovUkFooterMetaLink Href="/terms" Text="Terms and conditions" />
    </MetaLinks>
</GovUkFooter>
```

## Accessibility

- Uses `<footer>` element with `role="contentinfo"`
- Meta links use proper list structure
- Screen readers announce "Support links" heading

## For AI Coding Agents

When implementing footers:

1. Place footer after all main content
2. Include required links: Help, Cookies, Accessibility, Contact
3. Use `GovUkFooterMetaLink` for the meta links section
4. `LicenceText` supports HTML for links
5. Navigation sections are optional

```razor
// Minimal footer
<GovUkFooter 
    LicenceText="Licensed under Open Government Licence v3.0" />

// Standard footer with links
<GovUkFooter 
    LicenceText="Licensed under Open Government Licence v3.0"
    CopyrightText="© Crown copyright"
    CopyrightUrl="https://example.gov.uk/copyright">
    <MetaLinks>
        <GovUkFooterMetaLink Href="/accessibility" Text="Accessibility" />
        <GovUkFooterMetaLink Href="/cookies" Text="Cookies" />
        <GovUkFooterMetaLink Href="/privacy" Text="Privacy" />
    </MetaLinks>
</GovUkFooter>

// With navigation sections
<GovUkFooter LicenceText="...">
    <Navigation>
        <div class="govuk-footer__section govuk-grid-column-one-third">
            <h2 class="govuk-footer__heading govuk-heading-m">Section</h2>
            <ul class="govuk-footer__list">
                <li class="govuk-footer__list-item">
                    <a class="govuk-footer__link" href="#">Link</a>
                </li>
            </ul>
        </div>
    </Navigation>
</GovUkFooter>
```
