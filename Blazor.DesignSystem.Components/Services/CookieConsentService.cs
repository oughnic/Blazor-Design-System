using Microsoft.JSInterop;

namespace Blazor.DesignSystem.Components.Services;

/// <summary>
/// Implementation of <see cref="ICookieConsentService"/> that stores consent in browser cookies using JavaScript interop.
/// </summary>
public class CookieConsentService : ICookieConsentService
{
    private const string ConsentCookieName = "cookies_preferences_set";
    private const string AnalyticsCookieName = "cookies_policy";
    private const int CookieExpiryDays = 365;

    private readonly IJSRuntime _jsRuntime;

    /// <summary>
    /// Creates a new instance of <see cref="CookieConsentService"/>.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime for interop.</param>
    public CookieConsentService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
    }

    /// <inheritdoc/>
    public async Task<bool> HasConsentAsync()
    {
        try
        {
            var value = await _jsRuntime.InvokeAsync<string>("GovUkDesignSystem.getCookie", ConsentCookieName);
            return value == "true";
        }
        catch (JSException)
        {
            // Cookie access may fail during prerendering or if JS is unavailable - treat as no consent
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> GetAnalyticsConsentAsync()
    {
        try
        {
            var value = await _jsRuntime.InvokeAsync<string>("GovUkDesignSystem.getCookie", AnalyticsCookieName);
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            // Parse the JSON value using System.Text.Json for better error handling
            var consent = System.Text.Json.JsonSerializer.Deserialize<CookiePolicy>(value);
            return consent?.Analytics ?? false;
        }
        catch (JSException)
        {
            // Cookie access may fail during prerendering or if JS is unavailable - treat as no consent
            return false;
        }
        catch (System.Text.Json.JsonException)
        {
            // Invalid JSON in cookie - treat as no consent
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task SetConsentAsync(bool acceptAnalytics)
    {
        try
        {
            var policy = new CookiePolicy { Analytics = acceptAnalytics };
            var policyJson = System.Text.Json.JsonSerializer.Serialize(policy);

            await _jsRuntime.InvokeVoidAsync(
                "GovUkDesignSystem.setCookie",
                AnalyticsCookieName,
                policyJson,
                CookieExpiryDays);

            await _jsRuntime.InvokeVoidAsync(
                "GovUkDesignSystem.setCookie",
                ConsentCookieName,
                "true",
                CookieExpiryDays);
        }
        catch (JSException)
        {
            // Cookie setting may fail during prerendering or if JS is unavailable
            // This is a non-critical failure - user can try again
        }
    }

    /// <inheritdoc/>
    public async Task ClearConsentAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("GovUkDesignSystem.deleteCookie", ConsentCookieName);
            await _jsRuntime.InvokeVoidAsync("GovUkDesignSystem.deleteCookie", AnalyticsCookieName);
        }
        catch (JSException)
        {
            // Cookie deletion may fail during prerendering or if JS is unavailable
            // This is a non-critical failure - user can try again
        }
    }

    private sealed class CookiePolicy
    {
        public bool Analytics { get; set; }
    }
}
