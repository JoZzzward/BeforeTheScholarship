namespace BeforeTheScholarship.Api.Configuration;

/// <summary>
/// Controllers and Views Configuration
/// </summary>
public static class ControllersAndViewsConfiguration
{
    /// <summary>
    /// Adds controller and views setup
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddControllersAndViews(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }

    /// <summary>
    /// Adds controller and views setup
    /// </summary>
    /// <param name="app"></param>
    public static void UseControllersAndViews(this WebApplication app)
    {
        app.MapControllers()
            .RequireCors(CorsSettings.DefaultOriginName);
    }

}
