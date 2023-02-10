namespace BeforeTheScholarship.Api.Configuration;

public static class AutoMapperConfiguration
{
    /// <summary>
    /// Adds AutoMapper to domain assemblies which names starts with "BEFORETHESCHOLARSHIP"
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName != null && x.FullName.ToUpper().StartsWith("BEFORETHESCHOLARSHIP"));

        services.AddAutoMapper(assemblies);

        return services;
    }
}
