namespace BeforeTheScholarship.Api.Configuration;

/// <summary>
/// CORS configuration
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Adds CORS to application 
    /// </summary>
    /// <param name="services">Services collection</param>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddPolicy(CorsSettings.DefaultOriginName, policy => 
            {
                policy.WithOrigins("http://localhost:7001",
                                   "http://localhost:7002")
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
            });
        });
        return services;
    }

    /// <summary>
    /// Adds CORS using to application
    /// </summary>
    /// <param name="app">Application</param>
    public static void UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsSettings.DefaultOriginName);
    }
}