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
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserManager<UserManager<StudentUser>>()
            .AddDefaultTokenProviders();

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
            