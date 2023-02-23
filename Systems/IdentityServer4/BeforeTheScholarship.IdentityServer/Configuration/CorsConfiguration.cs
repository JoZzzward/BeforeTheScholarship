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
                AllowAll = true
            };
        });

        return services;
    }

    /// <summary>
    /// Using App Cors
    /// </summary>
    /// <param name="app">Application</param>
    /// <returns></returns>
    public static void UseAppCors(this IApplicationBuilder app)
    {

    }
}
