using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Services.UserAccountService.Models;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.SignInManager;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.UserManger;
using FluentAssertions;

namespace BeforeTheScholarship.Tests.Unit.Services.UserAccountService
{
    [Trait("Category", "Unit")]
    public class UserAccountServiceTests
    {
        private readonly UserAccountServiceHelper _userAccountServiceHelper = new();
        private readonly IUserAccountService _userAccountService;

        public UserAccountServiceTests()
        {
            _userAccountService = _userAccountServiceHelper.Initialize();
        }

        [Fact]
        public async Task RegisterUser_UserNotExists_ReturnsRegisterUserAccountResponse()
        {
            // Arrange
            var model = new RegisterUserAccountModel
            {
                UserName = "Test",
                Email = "testemail@test.com",
                ConfirmPassword = "userpassword",
                Password = "userpassword"
            };

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.Setup.SetupCreateAsync();

            // Act
            var result = await _userAccountService.RegisterUser(model);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task RegisterUser_UserAlreadyExists_ReturnsNull()
        {
            // Arrange
            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.Setup.SetupFindByNameAsyncReturnsData();
            UserManagerInitializer.Setup.SetupCreateAsync();

            var model = new RegisterUserAccountModel
            {
                UserName = "username",
                Email = "testemail@test.com",
                ConfirmPassword = "userpassword",
                Password = "userpassword"
            };

            // Act
            var result = await _userAccountService.RegisterUser(model);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task LoginUser_UserExists_ReturnsData()
        {
            // Arrange
            var model = new LoginUserAccountModel
            {
                Email = StudentConsts.Email,
                Password = StudentConsts.Password
            };

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsData();
            SignInManagerInitializer.Setup.SetupPasswordSignInAsyncReturnsSuccess();

            // Act
            var result = await _userAccountService.LoginUser(model);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task LoginUser_IncorrectPassword_ReturnsNull()
        {
            // Arrange
            var model = new LoginUserAccountModel();

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsData();
            SignInManagerInitializer.Setup.SetupPasswordSignInAsyncReturnsFailed();

            // Act
            var result = await _userAccountService.LoginUser(model);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task LoginUser_UserNotFound_ReturnsNull()
        {
            // Arrange
            var model = new LoginUserAccountModel();

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsNull();

            // Act
            var result = await _userAccountService.LoginUser(model);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SendConfirmEmail_CorrectEmail_ReturnsSendConfirmationEmailResponse()
        {
            // Arrange
            var model = new SendConfirmationEmailModel
            {
                Email = "testemail@test.com"
            };

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.Setup.SetupGenerateEmailConfirmationTokenAsync();

            // Act
            var result = await _userAccountService.SendConfirmEmail(model);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task SendConfirmEmail_IncorrectEmail_ReturnsNull()
        {
            // Arrange
            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.Setup.SetupGenerateEmailConfirmationTokenAsync();

            var model = new SendConfirmationEmailModel
            {
                Email = StudentConsts.Email
            };

            // Act
            var result = await _userAccountService.SendConfirmEmail(model);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task ConfirmEmail_CorrectEmail_ReturnsConfirmationEmailResponse()
        {
            // Arrange
            var model = new ConfirmationEmailModel
            {
                Email = "testemail@test.com",
                Token = "fakeToken"
            };

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.Setup.SetupConfirmEmailAsync();

            // Act
            var result = await _userAccountService.ConfirmEmail(model);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task ConfirmEmail_InorrectEmail_ReturnsNull()
        {
            // Arrange
            var model = new ConfirmationEmailModel();

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.Setup.SetupConfirmEmailAsync();

            // Act
            var result = await _userAccountService.ConfirmEmail(model);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SendRecoveryPasswordEmail_CorrectEmail_ReturnsPasswordRecoveryResponse()
        {
            // Arrange
            var model = new SendPasswordRecoveryModel
            {
                Email = StudentConsts.Email
            };

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.Setup.SetupGeneratePasswordResetTokenAsync();

            // Act
            var result = await _userAccountService.SendRecoveryPasswordEmail(model);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task SendRecoveryPasswordEmail_InorrectEmail_ReturnsNull()
        {
            // Arrange
            var model = new SendPasswordRecoveryModel();

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.Setup.SetupGeneratePasswordResetTokenAsync();

            // Act
            var result = await _userAccountService.SendRecoveryPasswordEmail(model);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task RecoverPassword_CorrectEmail_ReturnsPasswordRecoveryResponse()
        {
            // Arrange
            var model = new PasswordRecoveryModel
            {
                Email = StudentConsts.Email
            };

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.Setup.SetupResetPasswordAsync();

            // Act
            var result = await _userAccountService.RecoverPassword(model);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task RecoverPassword_IncorrectEmail_ReturnsPasswordRecoveryResponse()
        {
            // Arrange
            var model = new PasswordRecoveryModel();

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.Setup.SetupResetPasswordAsync();

            // Act
            var result = await _userAccountService.RecoverPassword(model);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task ChangePassword_CorrectEmail_ReturnsChangePasswordResponse()
        {
            // Arrange
            var model = new ChangePasswordModel
            {
                Email = StudentConsts.Email,
                CurrentPassword = StudentConsts.Password,
                NewPassword = StudentConsts.NewPassword
            };

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.Setup.SetupGeneratePasswordResetTokenAsync();
            UserManagerInitializer.Setup.SetupResetPasswordAsync();

            // Act
            var result = await _userAccountService.ChangePassword(model);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task ChangePassword_IncorrectEmail_ReturnsNull()
        {
            // Arrange
            var model = new ChangePasswordModel();

            UserManagerInitializer.Setup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.Setup.SetupGeneratePasswordResetTokenAsync();
            UserManagerInitializer.Setup.SetupResetPasswordAsync();

            // Act
            var result = await _userAccountService.ChangePassword(model);

            // Assert
            result.Should().BeNull();
        }
    }
}
