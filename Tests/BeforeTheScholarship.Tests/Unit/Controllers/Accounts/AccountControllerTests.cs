using BeforeTheScholarship.Api.Controllers.Accounts;
using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Services.UserAccountService.Models;
using BeforeTheScholarship.Tests.Unit.Controllers.Accounts.Helpers;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using FluentAssertions;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Accounts
{
    public class AccountControllerTests
    {
        private readonly AccountsController _controller;

        private readonly IUserAccountService _userAccountService;

        private readonly AccountsDataHelper _accountsDataHelper = new();

        public AccountControllerTests()
        {
            _userAccountService = _accountsDataHelper.CreateUserAccountService();
            _controller = new AccountsController(
                _userAccountService,
                AutoMapperInitializer.Initialize(),
                LoggerInitializer.Initialize<AccountsController>());
        }

        [Fact]
        public async Task RegisterUser_WithoutData_ReturnsRegisterUserAccountResponse()
        {
            // Arrange
            var request = Substitute.For<RegisterUserAccountRequest>();
            request.Email = "user1@tst.com";
            request.UserName = "user1";
            request.Password = "aaaa1111";
            request.ConfirmPassword = "aaaa1111";
            
            var model = Substitute.For<RegisterUserAccountModel>();

            model.Email = "user1@tst.com";
            model.UserName = "user1";
            model.Password = "aaaa1111";
            model.ConfirmPassword = "aaaa1111";

            var response = Substitute.For<RegisterUserAccountResponse>();
            response.Email = "user1@tst.com";
            response.UserId = Guid.NewGuid().ToString();

            _userAccountService.RegisterUser(model).Returns(response);

            // Act
            var result = await _controller.Register(request);

            // Assert

            result.Should().NotBeNull();
            result.Email.Should().Be(model.Email);
            result.Should().BeAssignableTo(typeof(RegisterUserAccountResponse));
        }

        [Fact]
        public async Task LoginUser_WithoutData_ReturnsLoginUserAccountResponse()
        {
            // Arrange
            var request = Substitute.For<LoginUserAccountRequest>();

            var model = Substitute.For<LoginUserAccountModel>();

            var response = Substitute.For<LoginUserAccountResponse>();

            _userAccountService.LoginUser(model).Returns(response);

            // Act
            var result = await _controller.Login(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo(typeof(LoginUserAccountResponse));
        }

        [Fact]
        public async Task ConfirmEmail_WithoutData_ReturnsConfirmationEmailResponse()
        {
            // Arrange
            var request = Substitute.For<ConfirmationEmailRequest>();

            var model = Substitute.For<ConfirmationEmailModel>();

            var response = Substitute.For<ConfirmationEmailResponse>();

            _userAccountService.ConfirmEmail(model).Returns(response);

            // Act
            var result = await _controller.ConfirmEmail(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo(typeof(ConfirmationEmailResponse));
        }

        [Fact]
        public async Task SendRecoverPassword_WithoutData_ReturnsPasswordRecoveryResponse()
        {
            // Arrange
            var request = Substitute.For<PasswordRecoveryMailRequest>();

            var model = Substitute.For<PasswordRecoveryMailModel>();

            var response = Substitute.For<PasswordRecoveryResponse>();

            _userAccountService.SendRecoveryPasswordEmail(model).Returns(response);

            // Act
            var result = await _controller.SendRecoverPassword(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo(typeof(PasswordRecoveryResponse));
        }

        [Fact]
        public async Task RecoverPassword_WithoutData_ReturnsPasswordRecoveryResponse()
        {
            // Arrange
            var request = Substitute.For<PasswordRecoveryRequest>();

            var model = Substitute.For<PasswordRecoveryModel>();

            var response = Substitute.For<PasswordRecoveryResponse>();

            _userAccountService.RecoverPassword(model).Returns(response);

            // Act
            var result = await _controller.RecoverPassword(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo(typeof(PasswordRecoveryResponse));
        }

        [Fact]
        public async Task ChangePassword_WithoutData_ReturnsChangePasswordResponse()
        {
            // Arrange
            var request = Substitute.For<ChangePasswordRequest>();

            var model = Substitute.For<ChangePasswordModel>();

            var response = Substitute.For<ChangePasswordResponse>();

            _userAccountService.ChangePassword(model).Returns(response);

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo(typeof(ChangePasswordResponse));
        }
    }
}
