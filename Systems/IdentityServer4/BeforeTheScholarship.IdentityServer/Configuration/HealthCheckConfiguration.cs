using BeforeTheScholarship.IdentityServer.Configuration.HealthChecks;

namespace BeforeTheScholarship.IdentityServer.Configuration;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<HealthCheck>("BeforeTheScholarship.IdentityServer");

        return services;
    }

    public static void UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/healthy");
    }
}