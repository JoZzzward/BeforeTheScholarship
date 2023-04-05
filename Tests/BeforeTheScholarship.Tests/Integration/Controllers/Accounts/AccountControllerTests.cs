using BeforeTheScholarship.Api.Controllers.Accounts;
using BeforeTheScholarship.Api.Controllers.Accounts.Models;
using BeforeTheScholarship.Services.UserAccountService.Models;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Integration.Base.Helpers;
using BeforeTheScholarship.Tests.Integration.Controllers.Accounts.Helpers;
using BeforeTheScholarship.Tests.Integration.Core;
using FluentAssertions;

namespace BeforeTheScholarship.Tests.Integration.Controllers.Accounts
{
    [Trait("Category", "Integration")]
    public class AccountsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly DataHelper _sutDataHelper;

        public AccountsControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.SetupClient();

            _sutDataHelper = new AccountControllerDataHelper(factory.Services);
        }

        [Fact]
        public async Task RegisterUser_WithData_Returns200Response()
        {
            // Arrange
            var model = new RegisterUserAccountRequest
            {
                Email = StudentConsts.Email + Random.Shared.Next(1000, 10000),
                UserName = StudentConsts.UserName + Random.Shared.Next(1000, 10000),
                Password = StudentConsts.Password,
                ConfirmPassword = StudentConsts.Password
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);

            // Act
            var response = await _client.PostAsync("accounts/register", request);
            var content = _sutDataHelper.GenerateRequestFromModel<RegisterUserAccountResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email); 
        }

        [Fact]
        public async Task LoginUser_WithData_Returns200Response()
        {
            // Arrange
            var model = new LoginUserAccountRequest
            {
                Email = StudentConsts.Email,
                Password = StudentConsts.Password
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);

            // Act
            var response = await _client.PostAsync("accounts/login", request);
            var content = _sutDataHelper.GenerateRequestFromModel<LoginUserAccountResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task SendConfirmEmail_WithData_Returns200Response()
        {
            // Arrange
            var model = new SendConfirmationEmailRequest
            {
                Email = StudentConsts.Email
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);

            // Act
            var response = await _client.PostAsync("accounts/send-confirm-email", request);
            var content = _sutDataHelper.GenerateRequestFromModel<SendConfirmationEmailResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task ConfirmEmail_Returns200Response()
        {
            // Arrange;
            var token = await _sutDataHelper.GenerateUserConfirmationToken(StudentConsts.Email);
            var model = new ConfirmationEmailRequest
            {
                Email = StudentConsts.Email,
                Token = token
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);

            // Act
            var response = await _client.PostAsync("accounts/confirm-email", request);
            var content = _sutDataHelper.GenerateRequestFromModel<ConfirmationEmailResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task SendRecoverPassword_Returns200Response()
        {
            // Arrange;
            var model = new SendPasswordRecoveryRequest
            {
                Email = StudentConsts.Email
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);
            // Act
            var response = await _client.PostAsync("accounts/send-recover-password", request);
            var content = _sutDataHelper.GenerateRequestFromModel<PasswordRecoveryResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task RecoverPassword_Returns200Response()
        {
            // Arrange;
            var token = await _sutDataHelper.GenerateUserRecoveryPasswordToken(StudentConsts.Email);
            var model = new PasswordRecoveryRequest
            {
                Email = StudentConsts.Email,
                Token = token,
                NewPassword = StudentConsts.NewPassword
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);

            // Act
            var response = await _client.PostAsync("accounts/recover-password", request);
            var content = _sutDataHelper.GenerateRequestFromModel<PasswordRecoveryResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email);
        }

        [Fact]
        public async Task ChangePassword_Returns200Response()
        {
            // Arrange;
            var model = new ChangePasswordRequest
            {
                Email = StudentConsts.Email,
                CurrentPassword = StudentConsts.Password,
                NewPassword = StudentConsts.NewPassword
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);
            
            // Act
            var response = await _client.PostAsync("accounts/change-password", request);
            var content = _sutDataHelper.GenerateRequestFromModel<ChangePasswordResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email);
            content.UserName.Should().Be(StudentConsts.UserName);
        }
    }
}