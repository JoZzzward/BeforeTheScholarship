using BeforeTheScholarship.Services.UserAccountService;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Tests.Unit.Helpers.Configuration
{
    public static class LoggerInitializer
    {

        public static ILogger<T> Initialize<T>()
        {
            var loggerFactory = Substitute.For<LoggerFactory>();

            var logger = loggerFactory.CreateLogger<T>();

            return logger;
        }

    }
}
