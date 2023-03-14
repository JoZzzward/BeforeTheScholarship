using BeforeTheScholarship.Services.Settings;
using BeforeTheScholarship.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Services.RabbitMqService;

public static class Bootstrapper
{
    public static IServiceCollection AddRabbitMqService(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = AppSettings.Load<RabbitMqSettings>("RabbitMqSettings", configuration)!;
        services.AddSingleton(settings);

        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        return services;
    }
}