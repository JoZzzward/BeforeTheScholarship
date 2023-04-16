using BeforeTheScholarship.Api.Controllers.Debts;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.DebtService.Models;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Integration.Controllers.Debts.Helpers;
using BeforeTheScholarship.Tests.Integration.Core;
using BeforeTheScholarship.Tests.Unit.Controllers.Debts.Consts;
using FluentAssertions;

namespace BeforeTheScholarship.Tests.Integration.Controllers.Debts
{
    [Trait("Category", "Integration")]
    public class DebtsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly DebtsControllerDataHelper _sutDataHelper = new();

        public DebtsControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.SetupClient();
        }

        [Fact]
        public async Task GetAllDebts_WithData_Returns200Response()
        {
            // Arrange
            var url = "debts";

            // Act
            var response = await _client.GetAsync(url);

            var content = _sutDataHelper.GenerateContentFromModel<IEnumerable<DebtResponse>>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Count().Should().BeGreaterOrEqualTo(3);
            content.All(x => x.Borrowed >= 50).Should().BeTrue();
        }

        [Fact]
        public async Task GetDebtsByStudentId_WithData_Returns200Response()
        {
            // Arrange
            var url = $"debts/{StudentConsts.Id}";

            // Act
            var response = await _client.GetAsync(url);
            var content = _sutDataHelper.GenerateContentFromModel<IEnumerable<DebtResponse>>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Count().Should().BeGreaterOrEqualTo(1);
            content.All(x => 50 <= x.Borrowed).Should().BeTrue();
        }

        [Fact]
        public async Task GetOverdueDebts_WithData_Returns200Response()
        {
            // Arrange
            var url = $"debts/overdue?studentId={StudentConsts.Id}";

            // Act
            var response = await _client.GetAsync(url);
            var content = _sutDataHelper.GenerateContentFromModel<IEnumerable<DebtResponse>>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Count().Should().BeGreaterOrEqualTo(1);
            content.All(x => 50 <= x.Borrowed).Should().BeTrue();
            content.All(x => x.WhenToPayback.DateTime.Subtract(DateTime.UtcNow).TotalDays < 0);
        }

        [Fact]
        public async Task GetUrgentlyRepaidDebts_WithData_Returns200Response()
        {
            // Arrange
            var url = $"debts/urgently-repay?studentId={StudentConsts.Id}";
            
            // Act
            var response = await _client.GetAsync(url);
            var content = _sutDataHelper.GenerateContentFromModel<IEnumerable<DebtResponse>>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Count().Should().BeGreaterOrEqualTo(1);
            content.All(x => 50 <= x.Borrowed).Should().BeTrue();
        }

        [Fact]
        public async Task CreateDebt_WithData_Returns200Response()
        {
            // Arrange
            var url = "debts";

            var model = new CreateDebtRequest
            {
                StudentId = StudentConsts.Id,
                Borrowed = new Random().Next(50, 250),
                Phone = "11111111111",
                BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4)
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);

            // Act
            var response = await _client.PostAsync(url, request);
            var content = _sutDataHelper.GenerateContentFromModel<CreateDebtResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.StudentId.Should().Be(model.StudentId);
        }

        [Fact]
        public async Task UpdateDebt_WithData_Returns200Response()
        {
            // Arrange
            var url = $"debts/{ExistedDebtConsts.SecondUid}";

            var model = new UpdateDebtRequest
            {
                Borrowed = new Random().Next(50, 250),
                Phone = "11111111111",
                BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                WhenToPayback = DateTimeOffset.UtcNow.AddDays(5)
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);

            // Act
            var response = await _client.PutAsync(url, request);
            var content = _sutDataHelper.GenerateContentFromModel<UpdateDebtResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Uid.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteDebt_WithData_Returns200Response()
        {
            // Arrange
            var url = $"debts/{ExistedDebtConsts.SecondUid}";

            // Act
            var response = await _client.DeleteAsync(url);
            var content = _sutDataHelper.GenerateContentFromModel<DeleteDebtResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Uid.Should().NotBeNull();
        }
    }
}
