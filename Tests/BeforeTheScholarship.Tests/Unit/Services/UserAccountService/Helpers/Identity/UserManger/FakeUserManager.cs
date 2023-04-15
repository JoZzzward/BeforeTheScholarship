using BeforeTheScholarship.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.UserManger
{
    public class FakeUserManager : UserManager<StudentUser>
    {
        public FakeUserManager()
            : base(Substitute.For<IUserStore<StudentUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<IPasswordHasher<StudentUser>>(),
                new IUserValidator<StudentUser>[0],
                new IPasswordValidator<StudentUser>[0],
                Substitute.For<ILookupNormalizer>(),
                Substitute.For<IdentityErrorDescriber>(),
                Substitute.For<IServiceProvider>(),
                Substitute.For<ILogger<UserManager<StudentUser>>>())
        { }

    }
}
