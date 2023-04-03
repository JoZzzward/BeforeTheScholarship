using BeforeTheScholarship.Api.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Core.Setup
{
    public static class ConfigurationSetupExtensions
    {
        public static void ConfigurationSetup(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddAppCors();

            services.AddAppHealthChecks("Tests");
            services.AddAppVersioning();

            services.AddAppAutoMapper();

            services.AddControllersAndViews();

            services.AddValidator();
        }
    }
}
