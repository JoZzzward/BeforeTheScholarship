using BeforeTheScholarship.Common.Security;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace BeforeTheScholarship.IdentityServer.Configuration.Settings;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =
        new List<ApiScope>()
        {
            new ApiScope(name: AppScopes.DebtsRead,
                         displayName: "Access to read debts."),
            new ApiScope(name: AppScopes.DebtsWrite, 
                         displayName: "Access to write debts.")
        };

}
