using BeforeTheScholarship.Context.Factories;
using BeforeTheScholarship.Context.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Context;

public static class Bootstrapper
{
    /// <summary>
    /// Register database context as service
    /// </summary>
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = BeforeTheScholarship.Settings.Settings.Load<DbSettings>("Database", configuration);

        services.AddSingleton(settings);

        var dbOptionsBuilder = DbContextOptionsFactory.Configure(
            settings.ConnectionString,
            settings.Type);

        services.AddDbContextFactory<AppDbContext>(dbOptionsBuilder);

        return services;
    }
}
