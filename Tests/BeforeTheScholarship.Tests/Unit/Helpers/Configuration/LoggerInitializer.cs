using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
public static class LoggerInitializer
{
    public static ILogger<T> InitializeForType<T>() => Substitute.For<LoggerFactory>().CreateLogger<T>();
}
