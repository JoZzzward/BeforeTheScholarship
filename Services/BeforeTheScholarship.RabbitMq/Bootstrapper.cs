using BeforeTheScholarship.Services.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.RabbitMq;

public static class Bootstrapper
{
    public static IServiceCollection AddRabbitMqService(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Settings.Load<RabbitMqSettings>("RabbitMqSettings", configuration)!;
        services.AddSingleton(settings);

        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        return services;
    }
}