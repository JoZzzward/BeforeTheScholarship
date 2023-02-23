using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Configuration;

/// <summary>
/// Versioning configuration
/// </summary>
public static class VersioningConfiguration
{
    /// <summary>
    /// Adds application versioning
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddAppVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
