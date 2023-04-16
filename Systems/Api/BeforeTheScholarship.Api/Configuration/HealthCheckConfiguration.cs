using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Api.Configuration;

/// <summary>
/// HealthCheck Configuration
/// </summary>
public static class HealthCheckConfiguration
{
    /// <summary>
    /// Adds healthchecks settings
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<HealthCheck>("BeforeTheScholarship.Api");
        
        return services;
    }

    /// <summary>
    /// Adds healthchecks endpoints 
    /// </summary>
    /// <param name="app"></param>
    public static void UseHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/healthy");
    }
}