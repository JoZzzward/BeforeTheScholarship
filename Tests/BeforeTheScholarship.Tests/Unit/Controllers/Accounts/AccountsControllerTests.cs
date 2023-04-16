using BeforeTheScholarship.Api.Controllers.Accounts;
using BeforeTheScholarship.Api.Controllers.Accounts.Models;
using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Services.UserAccountService.Models;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Unit.Controllers.Accounts.Helpers;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Accounts
{
    [Trait("Category", "Unit")]
    public class AccountsControllerTests
    {
        private readonly AccountsController _controller;

        public AccountsControllerTests()
        {
            _controller = new AccountsController(
                AccountsControllerHelper.Initialize(),
                AutoMapperInitializer.Initialize(),
                LoggerInitializer.InitializeForType<AccountsController>()
                );

        }

        [Fact]
        public async Task RegisterUser_WithData_ReturnsRegisterUserAccountResponse()
        {
            // Arrange
            AccountsControllerHelper.Setup.SetupRegisterUserReturnsData();

            var request = new RegisterUserAccountRequest
            {
                Email = StudentConsts.Email,
                UserName = StudentConsts.UserName,
                ConfirmPassword = StudentConsts.Password,
                Password = StudentConsts.Password
            };

            // Act 
            var result = await _controller.Register(request);
            var resultContent = (result.Result as OkObjectResult).Value as RegisterUserAccountResponse;

            // Assert
            result.Should().NotBeNull();
            resultContent.UserId.Should().Be(StudentConsts.Id.ToString());
            resultContent.Email.Should().Be(StudentConsts.Email);
        }

        [Fact]
        public async Task LoginUser_WithData_ReturnsRegisterUserAccountResponse()
        {
            // Arrange
            AccountsControllerHelper.Setup.SetupLoginUserReturnsData();

            var request = new LoginUserAccountRequest
            {
                Email = StudentConsts.Email,
                Password = StudentConsts.Password
            };

            // Act 
            var result = await _controller.Login(request);
            var resultContent = (result.Result as OkObjectResult).Value as LoginUserAccountResponse;

            // Assert
            result.Should().NotBeNull();
            resultContent.Email.Should().Be(StudentConsts.Email);
        }

        [Fact]
        public async Task SendConfirmEmail_WithData_ReturnsSendConfirmationEmailResponse()
        {
            // Arrange
            AccountsControllerHelper.Setup.SetupSendConfirmEmailReturnsData();

            var request = new SendConfirmationEmailRequest
            {
                Email = StudentConsts.Email
            };

            // Act 
            var result = await _controller.SendConfirmEmail(request);
            var resultContent = (result.Result as OkObjectResult).Value as SendConfirmationEmailResponse;

            // Assert
            result.Should().NotBeNull();
            resultContent.Email.Should().Be(StudentConsts.Email);
        }

        [Fact]
        public async Task ConfirmEmail_WithData_ReturnsConfirmationEmailResponse()
        {
            // Arrange
            AccountsControllerHelper.Setup.SetupConfirmEmailReturnsData();

            var request = new ConfirmationEmailRequest
            {
                Email = StudentConsts.Email
            };

            // Act 
            var result = await _controller.ConfirmEmail(request);
            var resultContent = (result.Result as OkObjectResult).Value as ConfirmationEmailResponse;

            // Assert
            result.Should().NotBeNull();
            resultContent.Email.Should().Be(StudentConsts.Email);
        }

        [Fact]
        public async Task SendRecoveryPasswordEmail_WithData_ReturnsPasswordRecoveryResponse()
        {
            // Arrange
            AccountsControllerHelper.Setup.SetupSendRecoveryPasswordEmailReturnsData();

            var request = new SendPasswordRecoveryRequest()
            {
                Email = StudentConsts.Email
            };
            
            // Act 
            var result = await _controller.SendRecoverPassword(request);
            var resultContent = (result.Result as OkObjectResult).Value as PasswordRecoveryResponse;

            // Assert
            result.Should().NotBeNull();
            resultContent.Email.Should().Be(StudentConsts.Email);
        }

        [Fact]
        public async Task RecoverPassword_WithData_ReturnsPasswordRecoveryResponse()
        {
            // Arrange
            AccountsControllerHelper.Setup.SetupRecoverPasswordReturnsData();

            var request = new PasswordRecoveryRequest()
            {
                Email = StudentConsts.Email
            };

            // Act 
            var result = await _controller.RecoverPassword(request);
            var resultContent = (result.Result as OkObjectResult).Value as PasswordRecoveryResponse;

            // Assert
            result.Should().NotBeNull();
            resultContent.Email.Should().Be(StudentConsts.Email);
        }

        [Fact]
        public async Task ChangePassword_WithData_ReturnsChangePasswordResponse()
        {
            // Arrange
            AccountsControllerHelper.Setup.SetupChangePasswordReturnsData();

            var request = new ChangePasswordRequest
            {
                Email = StudentConsts.Email
            };

            // Act 
            var result = await _controller.ChangePassword(request);
            var resultContent = (result.Result as OkObjectResult).Value as ChangePasswordResponse;

            // Assert
            result.Should().NotBeNull();
            resultContent.Email.Should().Be(StudentConsts.Email);
        }
    }
}
