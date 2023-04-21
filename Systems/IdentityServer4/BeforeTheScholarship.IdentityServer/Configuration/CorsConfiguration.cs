using Duende.IdentityServer.Services;

namespace BeforeTheScholarship.IdentityServer.Configuration;

/// <summary>
/// Identity Cors Configuration
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Adding Identity App Cors 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddSingleton<ICorsPolicyService>((container) =>
        {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();

            return new DefaultCorsPolicyService(logger) 
            {
                AllowedOrigins = new[]
                {
                    "http://localhost:7000",
                    "http://localhost:7002"
                }
            };
        });

        return services;
    }
}
