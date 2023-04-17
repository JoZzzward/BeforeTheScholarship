using Microsoft.AspNetCore.Authorization;

namespace BeforeTheScholarship.Tests.Integration.Core.Authorization
{
    public class MaintenanceModeDisabledOrAuthenticatedUserRequirement : IAuthorizationRequirement
    { }
}
