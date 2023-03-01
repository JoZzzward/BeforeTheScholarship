using Duende.IdentityServer.Models;

namespace BeforeTheScholarship.IdentityServer.Configuration.Settings;

public static class AppIdentityResources
{
    public static IEnumerable<IdentityResource> IdentityResources =
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };
}