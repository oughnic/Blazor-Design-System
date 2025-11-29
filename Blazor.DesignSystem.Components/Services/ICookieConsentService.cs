namespace Blazor.DesignSystem.Components.Services;

/// <summary>
/// Defines operations for managing cookie consent state.
/// </summary>
public interface ICookieConsentService
{
    /// <summary>
    /// Gets whether the user has made a cookie consent choice.
    /// </summary>
    Task<bool> HasConsentAsync();

    /// <summary>
    /// Gets whether analytics cookies have been accepted.
    /// </summary>
    Task<bool> GetAnalyticsConsentAsync();

    /// <summary>
    /// Sets the user's cookie consent preference.
    /// </summary>
    /// <param name="acceptAnalytics">Whether the user accepts analytics cookies.</param>
    Task SetConsentAsync(bool acceptAnalytics);

    /// <summary>
    /// Clears all cookie consent state.
    /// </summary>
    Task ClearConsentAsync();
}
