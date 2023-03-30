using BeforeTheScholarship.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Accounts.Helpers
{
    public static class SignInManagerInitializer
    {
        public static FakeSignInManager Initialize()
        {
            var signInManager = Substitute.For<FakeSignInManager>();

            var signInSuccess = SignInResult.Success;
            signInManager
                .PasswordSignInAsync(Arg.Any<StudentUser>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
                .Returns(signInSuccess);

            return signInManager;
        }
    }

    public class FakeSignInManager : SignInManager<StudentUser>
    {
        public FakeSignInManager()
            : base(Substitute.For<FakeUserManager>(),
                new HttpContextAccessor(),
                Substitute.For<IUserClaimsPrincipalFactory<StudentUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<ILogger<SignInManager<StudentUser>>>(),
                Substitute.For<IAuthenticationSchemeProvider>(),
                Substitute.For<IUserConfirmation<StudentUser>>())
        { }
    }
}
