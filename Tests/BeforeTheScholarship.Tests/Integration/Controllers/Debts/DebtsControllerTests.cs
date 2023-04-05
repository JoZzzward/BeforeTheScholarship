using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Integration.Base.Helpers;
using BeforeTheScholarship.Tests.Integration.Controllers.Debts.Helpers;
using BeforeTheScholarship.Tests.Integration.Core;
using FluentAssertions;
using Testcontainers.Redis;
using Xunit.Abstractions;

namespace BeforeTheScholarship.Tests.Integration.Controllers.Debts
{
    [Trait("Category", "Integration")]
    public class DebtsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly DataHelper _sutDataHelper;
        private readonly RedisContainer _redisContainer = new RedisBuilder()
            .WithImage("redis")
            .WithHostname("localhost")
            .WithExposedPort(6379)
            .Build();

        public DebtsControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper)
        {
            _client = factory.SetupClient();

            _sutDataHelper = new DebtsControllerDataHelper();

            _redisContainer.StartAsync();
        }

        [Fact]
        public async Task GetAllDebts_WithData_Returns200Response()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("debts");

            var content = _sutDataHelper.GenerateRequestFromModel<IEnumerable<DebtResponse>>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Count().Should().BeGreaterOrEqualTo(3);
        }

        [Fact]
        public async Task GetDebtsByStudentId_WithData_Returns200Response()
        {
            // Arrange
            var url = $"debts/{StudentConsts.Id}";

            // Act
            var response = await _client.GetAsync(url);
            var content = _sutDataHelper.GenerateRequestFromModel<IEnumerable<DebtResponse>>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Count().Should().BeGreaterOrEqualTo(1);
            content.ToArray()[0].Id.Should().BeGreaterOrEqualTo(1);
        }

    }
}
