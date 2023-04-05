using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Integration.Base.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Controllers.Accounts.Helpers
{
    internal class AccountControllerDataHelper : DataHelper
    {
        private readonly IServiceProvider _serviceProvider;

        public AccountControllerDataHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<string> GenerateUserConfirmationToken(string email)
        {
            var user = await FindUserByEmailAsync(email);
            var userManager = await CreateUserManagerInstanceAsync();
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            return token;
        }

        public override async Task<string> GenerateUserRecoveryPasswordToken(string email)
        {
            var user = await FindUserByEmailAsync(email);
            var userManager = await CreateUserManagerInstanceAsync();
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        private async Task<StudentUser> FindUserByEmailAsync(string email)
        {
            var userManager = await CreateUserManagerInstanceAsync();
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                throw new NullReferenceException(
                    $"--> User (Email: {email}) was not found at: async Task<StudentUser?> FindUserByEmailAsync(string email)");

            return user;
        }

        private async Task<UserManager<StudentUser>> CreateUserManagerInstanceAsync()
        {
            var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<StudentUser>>();

            return userManager;
        }
    }
}
