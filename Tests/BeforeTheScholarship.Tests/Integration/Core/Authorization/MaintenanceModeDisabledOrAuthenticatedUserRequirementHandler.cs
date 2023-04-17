using Microsoft.AspNetCore.Authorization;

namespace BeforeTheScholarship.Tests.Integration.Core.Authorization
{
    public class MaintenanceModeDisabledOrAuthenticatedUserRequirementHandler : AuthorizationHandler<MaintenanceModeDisabledOrAuthenticatedUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceModeDisabledOrAuthenticatedUserRequirement requirement)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
