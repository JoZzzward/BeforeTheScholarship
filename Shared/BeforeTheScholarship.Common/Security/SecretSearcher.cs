using Microsoft.Extensions.Configuration;

namespace BeforeTheScholarship.Common.Security;

/// <summary>
/// Searches secrets in development and production modes.
/// </summary>
public static class SecretSearcher
{
    /// <summary>
    /// Searching secret by given keys - first checks by <paramref name="configurationKey"/>, than <paramref name="productionKey"/>.
    /// </summary>
    /// <param name="configurationKey">Key of configuration secret section</param>
    /// <param name="productionKey">Key of production secret value</param>
    public static string SearchSecret(string configurationKey, string productionKey, IConfiguration configuration = null)
    {
        var secretValue = configuration[configurationKey];

        if (!string.IsNullOrEmpty(secretValue))
            return secretValue;

        string path = "/run/secrets/" + productionKey;

        secretValue = File.ReadAllText(path);

        return secretValue;
    }
}
