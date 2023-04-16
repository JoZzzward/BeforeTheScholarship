using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Services.Actions
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddActionsService(this IServiceCollection services)
        {
            services.AddSingleton<IActionsService, ActionsService>();

            return services;
        }
    }
}
