using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Services.UserAccountService.Models;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.SignInManager;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.UserManger;
using FluentAssertions;

namespace BeforeTheScholarship.Tests.Unit.Services.UserAccountService
{
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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.UserManagerSetup.SetupCreateAsync();

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
            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.UserManagerSetup.SetupCreateAsync();

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
                Email = "testemail@test.com",
                Password = "userpassword"
            };

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            SignInManagerInitializer.SignInManagerSetup.SetupPasswordSignInAsyncReturnsSuccess();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            SignInManagerInitializer.SignInManagerSetup.SetupPasswordSignInAsyncReturnsFailed();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsNull();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.UserManagerSetup.SetupGenerateEmailConfirmationTokenAsync();

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
            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.UserManagerSetup.SetupGenerateEmailConfirmationTokenAsync();

            var model = new SendConfirmationEmailModel();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.UserManagerSetup.SetupConfirmEmailAsync();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.UserManagerSetup.SetupConfirmEmailAsync();

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
                Email = "testemail@test.com"
            };

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.UserManagerSetup.SetupGeneratePasswordResetTokenAsync();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.UserManagerSetup.SetupGeneratePasswordResetTokenAsync();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.UserManagerSetup.SetupResetPasswordAsync();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.UserManagerSetup.SetupResetPasswordAsync();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsData();
            UserManagerInitializer.UserManagerSetup.SetupGeneratePasswordResetTokenAsync();
            UserManagerInitializer.UserManagerSetup.SetupResetPasswordAsync();

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

            UserManagerInitializer.UserManagerSetup.SetupFindByEmailAsyncReturnsNull();
            UserManagerInitializer.UserManagerSetup.SetupGeneratePasswordResetTokenAsync();
            UserManagerInitializer.UserManagerSetup.SetupResetPasswordAsync();

            // Act
            var result = await _userAccountService.ChangePassword(model);

            // Assert
            result.Should().BeNull();
        }
    }
}
