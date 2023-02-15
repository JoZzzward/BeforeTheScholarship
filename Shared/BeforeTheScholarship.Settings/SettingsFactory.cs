namespace BeforeTheScholarship.Settings;

using Microsoft.Extensions.Configuration;

public static class SettingsFactory
{
    /// <summary>
    /// Creates a configuration file for reading environment variables from {appsettings.json} and {appsettings.Development.json} optional
    /// </summary>
    public static IConfiguration Create(IConfiguration? configuration = null)
    {
        var conf = configuration ?? new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: false)
                                        .AddJsonFile("appsettings.Development.json", optional: true)
                                        .AddEnvironmentVariables()
                                        .Build();

        return conf;
    }
}