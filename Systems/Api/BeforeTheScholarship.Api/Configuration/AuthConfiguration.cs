namespace BeforeTheScholarship.Api.Configuration;

using BeforeTheScholarship.Common.Security;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Authentication and Authorization configuration
/// </summary>
public static class AuthConfiguration
{
    /// <summary>
    /// Adds Authentication and Authorization to services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="settings"></param>
    public static IServiceCollection AddAppAuth(this IServiceCollection services, IdentitySettings settings)
    {
        IdentityModelEventSource.ShowPII = true;

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

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = settings.Url.StartsWith("https://");
                options.Authority = settings.Url;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = "api";
            });


        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppScopes.DebtsRead, policy => policy.RequireClaim("scope", AppScopes.DebtsRead));
            options.AddPolicy(AppScopes.DebtsWrite, policy => policy.RequireClaim("scope", AppScopes.DebtsWrite));
            options.AddPolicy(AppScopes.OpenId, policy => policy.RequireClaim("scope", AppScopes.OpenId));
            options.AddPolicy(AppScopes.Profile, policy => policy.RequireClaim("scope", AppScopes.Profile));
            options.AddPolicy(AppScopes.Email, policy => policy.RequireClaim("scope", AppScopes.Email));
        });

        return services;
    }

    /// <summary>
    /// Adds Authentication and Authorization to application
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
            