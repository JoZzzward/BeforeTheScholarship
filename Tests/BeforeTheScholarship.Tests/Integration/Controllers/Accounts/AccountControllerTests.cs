using BeforeTheScholarship.Api.Controllers.Accounts;
using BeforeTheScholarship.Services.UserAccountService.Models;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Integration.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BeforeTheScholarship.Tests.Integration.Controllers.Accounts
{
    [Trait("Category", "Integration")]
    public class AccountsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AccountsControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
            _client.BaseAddress = new Uri("http://localhost:7000");
            _client.DefaultRequestVersion = new Version("1.0");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task RegisterUser_Returns200Response()
        {
            // Arrange
            var model = new RegisterUserAccountRequest
            {
                Email = StudentConsts.Email + Random.Shared.Next(1000, 10000),
                UserName = StudentConsts.UserName + Random.Shared.Next(1000, 10000),
                Password = StudentConsts.Password,
                ConfirmPassword = StudentConsts.Password
            };

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/v1/accounts/register", request);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var content = JsonSerializer.Deserialize<RegisterUserAccountResponse>(responseContent);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email); 
        }

        [Fact]
        public async Task LoginUser_Returns200Response()
        {
            // Arrange
            var model = new LoginUserAccountRequest
            {
                Email = StudentConsts.Email,
                Password = StudentConsts.Password
            };

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/v1/accounts/login", request);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var content = JsonSerializer.Deserialize<LoginUserAccountResponse>(responseContent);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Email.Should().Be(model.Email);
        }
    }
}