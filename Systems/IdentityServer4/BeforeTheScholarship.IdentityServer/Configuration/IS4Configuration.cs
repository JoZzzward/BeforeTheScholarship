using BeforeTheScholarship.Common.Security;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.IdentityServer.Configuration.Settings;
using Microsoft.AspNetCore.Identity;

namespace BeforeTheScholarship.IdentityServer.Configuration;

public static class IS4Configuration
{
    public static IServiceCollection AddIS4(this IServiceCollection services)
    {
        services
            .AddIdentity<StudentUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserManager<UserManager<StudentUser>>()
            .AddSignInManager<SignInManager<StudentUser>>()
            .AddDefaultTokenProviders()
            .AddTokenProvider(IdentityProviderInfo.Name, typeof(DataProtectorTokenProvider<StudentUser>))
            ;

        services.AddIdentityServer()
            .AddAspNetIdentity<StudentUser>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator<StudentUser>>()
            .AddInMemoryClients(AppApiClients.Get(services.BuildServiceProvider()))
            .AddInMemoryIdentityResources(AppIdentityResources.IdentityResources)

            .AddInMemoryApiResources(AppApiResources.ApiResources)
            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)

            //.AddTestUsers(AppTestUsers.TestUsers)

            .AddDeveloperSigningCredential();

        return services;
    }

    public static IApplicationBuilder UseIS4(this IApplicationBuilder app)
    {
        app.UseIdentityServer();

        return app;
    }
}
