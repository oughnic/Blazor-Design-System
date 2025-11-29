using Blazor.DesignSystem.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.DesignSystem.Components;

/// <summary>
/// Extension methods for configuring GOV.UK Design System services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the GOV.UK Design System cookie consent service to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddGovUkCookieConsent(this IServiceCollection services)
    {
        services.AddScoped<ICookieConsentService, CookieConsentService>();
        return services;
    }
}
