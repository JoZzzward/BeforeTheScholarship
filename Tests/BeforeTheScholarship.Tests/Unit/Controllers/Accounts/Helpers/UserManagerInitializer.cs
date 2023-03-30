using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Unit.Helpers.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Accounts.Helpers
{
    public static class UserManagerInitializer
    {
        private static readonly DbContextHelper _dbContextHelper = new();

        public static FakeUserManager Initialize()
        {
            var context = _dbContextHelper.GetContextData(false);
            var success = IdentityResult.Success;
            var str = new string('A', 20);
            var userManager = Substitute.For<FakeUserManager>();
            var student = new StudentUser();

            userManager.Users.ToList().AddRange(context.StudentUsers);
            userManager.CreateAsync(Arg.Any<StudentUser>(), Arg.Any<string>()).Returns(success);
            userManager.ConfirmEmailAsync(Arg.Any<StudentUser>(), Arg.Any<string>()).Returns(success);
            userManager.GeneratePasswordResetTokenAsync(Arg.Any<StudentUser>()).Returns(Task.FromResult(str));
            userManager.ResetPasswordAsync(Arg.Any<StudentUser>(), Arg.Any<string>(), Arg.Any<string>()).Returns(success);
            userManager.GenerateEmailConfirmationTokenAsync(Arg.Any<StudentUser>()).Returns(str);
            userManager.FindByEmailAsync(Arg.Any<string>())!.Returns(Task.FromResult(student));

            return userManager;
        }
    }
    public class FakeUserManager : UserManager<StudentUser>
    {
        public FakeUserManager()
            : base(Substitute.For<IQueryableUserStore<StudentUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<IPasswordHasher<StudentUser>>(),
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                null,
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                null,
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                Substitute.For<ILookupNormalizer>(),
                Substitute.For<IdentityErrorDescriber>(),
                Substitute.For<IServiceProvider>(),
                Substitute.For<ILogger<UserManager<StudentUser>>>())
        { }
    }
}
