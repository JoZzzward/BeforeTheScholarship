using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using Microsoft.AspNetCore.Identity;
using NSubstitute.ClearExtensions;
using NSubstitute.ReturnsExtensions;

namespace BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.UserManger
{
    public static class UserManagerInitializer
    {
        private static FakeUserManager _userManager;

        public static UserManager<StudentUser> Initialize()
        {
            _userManager = Substitute.For<FakeUserManager>();

            return _userManager;
        }

        public static class UserManagerSetup
        {
            private static readonly IdentityResult identityResultSuccess = Task.FromResult(IdentityResult.Success).Result;
            private static readonly UserAccountServiceDataHelper _userAccountServiceDataHelper = new();

            public static void SetupCreateAsync()
            {
                _userManager.CreateAsync(Arg.Any<StudentUser>(), Arg.Any<string>()).Returns(identityResultSuccess);
            }

            public static void SetupFindByEmailAsyncReturnsData()
            {
                var student = _userAccountServiceDataHelper.GenerateStudentUserMoqModel();

                _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(student);
            }

            public static void SetupFindByEmailAsyncReturnsNull()
            {
                _userManager.FindByEmailAsync(Arg.Any<string>()).ReturnsNull();
            }

            public static void SetupGenerateEmailConfirmationTokenAsync()
            {
                _userManager.GenerateEmailConfirmationTokenAsync(Arg.Any<StudentUser>()).Returns("tokenValue");
            }

            public static void SetupGeneratePasswordResetTokenAsync()
            {
                _userManager.GeneratePasswordResetTokenAsync(Arg.Any<StudentUser>()).Returns("tokenValue");
            }

            public static void SetupResetPasswordAsync()
            {
                _userManager.ResetPasswordAsync(Arg.Any<StudentUser>(), Arg.Any<string>(), Arg.Any<string>()).Returns(identityResultSuccess);
            }

            public static void SetupConfirmEmailAsync()
            {
                _userManager.ConfirmEmailAsync(Arg.Any<StudentUser>(), Arg.Any<string>()).Returns(identityResultSuccess);
            }
        }
    }
}
