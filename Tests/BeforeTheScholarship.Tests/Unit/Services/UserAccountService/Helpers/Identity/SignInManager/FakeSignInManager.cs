using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.UserManger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.SignInManager
{
    public class FakeSignInManager : SignInManager<StudentUser>
    {
        public FakeSignInManager()
            : base(Substitute.For<FakeUserManager>(),
                new HttpContextAccessor(),
                Substitute.For<IUserClaimsPrincipalFactory<StudentUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                LoggerInitializer.InitializeForType<SignInManager<StudentUser>>(),
                null,
                null)
        {

        }
    }

}
