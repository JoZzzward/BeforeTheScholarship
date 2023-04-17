using BeforeTheScholarship.Common.Security;
using BeforeTheScholarship.Tests.Integration.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Core.Extensions
{
    public static class SecuritySetupExtensions
    {
        public static void SecuritySetup(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, MaintenanceModeDisabledOrAuthenticatedUserRequirementHandler>();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new MaintenanceModeDisabledOrAuthenticatedUserRequirement())
                    .Build();

                options.AddPolicy(AppScopes.DebtsRead,
                    builder => builder.AddRequirements(new MaintenanceModeDisabledOrAuthenticatedUserRequirement()));
                options.AddPolicy(AppScopes.DebtsWrite,
                    builder => builder.AddRequirements(new MaintenanceModeDisabledOrAuthenticatedUserRequirement()));
                options.AddPolicy(AppScopes.OpenId, builder => builder
                    .AddRequirements(new MaintenanceModeDisabledOrAuthenticatedUserRequirement()));
                options.AddPolicy(AppScopes.Profile, builder => builder
                    .AddRequirements(new MaintenanceModeDisabledOrAuthenticatedUserRequirement()));
                options.AddPolicy(AppScopes.Email, builder => builder
                    .AddRequirements(new MaintenanceModeDisabledOrAuthenticatedUserRequirement()));
            });
        }
    }
}
