using BeforeTheScholarship.Entities;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.SignInManager
{
    public static class SignInManagerInitializer
    {
        private static FakeSignInManager _signInManager;

        public static SignInManager<StudentUser> Initialize()
        {
            _signInManager = Substitute.For<FakeSignInManager>();

            return _signInManager;
        }

        public static class Setup
        {
            private static readonly SignInResult identityResultSuccess = Task.FromResult(SignInResult.Success).Result;
            private static readonly SignInResult identityResultFailed = Task.FromResult(SignInResult.Failed).Result;

            public static void SetupPasswordSignInAsyncReturnsSuccess()
            {
                _signInManager.PasswordSignInAsync(Arg.Any<StudentUser>(), Arg.Any<string>(), true, false)
                    .Returns(identityResultSuccess);
            }

            public static void SetupPasswordSignInAsyncReturnsFailed()
            {
                _signInManager.PasswordSignInAsync(Arg.Any<StudentUser>(), Arg.Any<string>(), true, false)
                    .Returns(identityResultFailed);
            }
        }
    }
}
