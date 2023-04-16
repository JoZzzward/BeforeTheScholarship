using BeforeTheScholarship.Services.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Helpers.ActionsService
{
    public static partial class Bootstrapper
    {
        public static void AddFakeActionsService(this IServiceCollection services)
        {
            var actionsService = ActionsServiceHelper.Initialize();

            services.AddSingleton(typeof(IActionsService), actionsService);
        }
    }
}
