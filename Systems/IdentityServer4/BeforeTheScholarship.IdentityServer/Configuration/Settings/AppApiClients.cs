using BeforeTheScholarship.Common.Security;
using Duende.IdentityServer.Models;
using System.Text;

namespace BeforeTheScholarship.IdentityServer.Configuration.Settings;

public static class AppApiClients
{
    public static IEnumerable<Client> ApiClients =
        new List<Client>()
            {
                new Client()
                {
                    ClientId = "swagger",
                    ClientSecrets =
                    {
                        new Secret("acrosstheapi".Sha256())
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
                        new Secret("acrosstheapi".Sha256())
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

}
