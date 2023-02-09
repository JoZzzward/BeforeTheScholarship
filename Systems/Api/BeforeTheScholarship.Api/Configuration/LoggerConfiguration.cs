using Serilog;

namespace BeforeTheScholarship.Api.Configuration;

public static class LoggerConfiguration
{
    public static void AddLogger(this WebApplicationBuilder builder)
    {
        var logger = new Serilog.LoggerConfiguration()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} | INFO | ({CorrelationId})] {Message}{NewLine}{Exception}"
            )
            .WriteTo.File(
                path: "errorlog.txt",
                outputTemplate: "[{Timestamp:HH:mm:ss} | WARN | ({CorrelationId})] {Message}{NewLine}{Exception}",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
            .CreateLogger();

        builder.Host.UseSerilog(logger, true);
    }
}
