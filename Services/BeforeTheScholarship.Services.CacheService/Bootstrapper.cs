using BeforeTheScholarship.Services.Settings;
using BeforeTheScholarship.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Services.CacheService;

public static class Bootstrapper
{
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = AppSettings.Load<CacheSettings>("CacheSettings", configuration);
        services.AddSingleton(settings);

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
