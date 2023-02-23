namespace BeforeTheScholarship.Api.Configuration;

/// <summary>
/// AutoMapper Configuration
/// </summary>
public static class AutoMapperConfiguration
{
    /// <summary>
    /// Adds AutoMapper to domain assemblies which names starts with "beforethescholarship."
    /// </summary>
    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("beforethescholarship."));

        services.AddAutoMapper(assemblies);

        return services;
    }
}
