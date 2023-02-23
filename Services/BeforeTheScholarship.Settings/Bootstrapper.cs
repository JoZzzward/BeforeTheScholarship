namespace BeforeTheScholarship.Services.Settings;

using BeforeTheScholarship.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    
    public static IServiceCollection AddIdentitySettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<IdentitySettings>("IdentitySettings", configuration);
        services.AddSingleton(settings!);

        return services;
    }

    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<SwaggerSettings>("SwaggerSettings", configuration);
        services.AddSingleton(settings!);

        return services;
    }
}