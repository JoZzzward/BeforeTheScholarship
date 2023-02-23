using Duende.IdentityServer.Models;

namespace BeforeTheScholarship.IdentityServer.Configuration.Settings;

public static class AppApiResources
{
    public static IEnumerable<ApiResource> ApiResources = 
        new List<ApiResource>()
            {
                new ApiResource("api")
            };  
}
