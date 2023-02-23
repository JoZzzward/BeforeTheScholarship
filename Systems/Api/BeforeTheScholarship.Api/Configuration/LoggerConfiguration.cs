using Serilog;

namespace BeforeTheScholarship.Api.Configuration;

/// <summary>
/// Logger configuration
/// </summary>
public static class LoggerConfiguration
{
    /// <summary>
    /// Adds Serilog logger to application
    /// </summary>
    /// <param name="builder"></param>
    public static void AddLogger(this WebApplicationBuilder builder)
    {
        var logger = new Serilog.LoggerConfiguration()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog(logger, true);
    }
}
