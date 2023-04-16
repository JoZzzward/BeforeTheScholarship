using BeforeTheScholarship.Services.RabbitMqService;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Helpers.RabbitMqService
{
    public static partial class Bootstrapper
    {
        public static void AddFakeRabbitMqService(this IServiceCollection services)
        {
            var rabbitMqService = RabbitMqServiceHelper.Initialize();

            services.AddSingleton(typeof(IRabbitMqService), rabbitMqService);
        }
    }
}
