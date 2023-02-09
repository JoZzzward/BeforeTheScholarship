namespace BeforeTheScholarship.Api;

/// <summary>
/// Helps to register all services in application
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds all services to app
    /// </summary>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        //services.Add.... etc.

        return services;
    }


}
