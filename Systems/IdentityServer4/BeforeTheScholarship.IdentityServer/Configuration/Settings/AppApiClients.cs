﻿using BeforeTheScholarship.Common.Security;
using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace BeforeTheScholarship.IdentityServer.Configuration.Settings;

public static class AppApiClients
{
    public static IEnumerable<Client> Get(ServiceProvider provider)
    {
        var configuration = provider.GetRequiredService<IConfiguration>();

        string clientSecret = SecretSearcher.SearchSecret("ClientSecretValue:IdentitySettings", "clientsecret", configuration);

        var clients = new List<Client>()
            {
                new Client()
                {
                    ClientId = "swagger",
                    ClientSecrets =
                    {
                        // ToString is important
                        new Secret(clientSecret.ToString().Sha256())
                    },
                    AccessTokenLifetime = 3600,

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes =
                    {
                        AppScopes.DebtsRead,
                        AppScopes.DebtsWrite
                    },
                },

                new Client()
                {
                    ClientId= "mobile_app",
                    ClientSecrets =
                    {
                        // ToString is important
                        new Secret(clientSecret.ToString().Sha256())
                    },

                    AccessTokenLifetime = 3600,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AbsoluteRefreshTokenLifetime= 2592000, // 30 days
                    SlidingRefreshTokenLifetime = 1296000, // 15 days

                    AllowedScopes =
                    {
                        AppScopes.DebtsRead,
                        AppScopes.DebtsWrite
                    }
                }
            };

        return clients;
    }
}
