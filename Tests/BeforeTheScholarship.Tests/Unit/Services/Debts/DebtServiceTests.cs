using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers;
using FluentAssertions;
using static BeforeTheScholarship.Tests.Unit.Helpers.Configuration.CacheServiceInitializer;

namespace BeforeTheScholarship.Tests.Unit.Services.Debts
{
    [Trait("Category", "Unit")]
    public class DebtServiceTests
    {
        private readonly IDebtService _sut = _sutHelper.CreateDebtService();
        private static readonly DebtServiceHelper _sutHelper = new();
        private readonly DebtsDataHelper _debtsDataHelper = new();

        [Fact]
        public async Task GetDebts_WithData_ReturnsIEnumerableOfDebtResponse()
        {
            // Arrange
            CacheServiceSetup.SetupGetStringReturnsNull();

            // Act
            var result = await _sut.GetDebts();

            // Assert

            result.Should().NotBeNull();
            result.Count().Should().Be(3);
            result.Any(x => 50 < x.Borrowed).Should().BeTrue();
        }

        [Fact]
        public async Task GetDebts_WithData_ReturnsIEnumerableOfDebtResponseFromCache()
        {
            // Arrange
            CacheServiceSetup.SetupGetStringReturnsData();

            // Act
            var result = await _sut.GetDebts();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(3);
            result.Any(x => 50 < x.Borrowed).Should().BeTrue();
        }

        [Fact]
        public async Task GetDebtsByStudentId_WithData_ReturnsIEnumerableOfDebtResponse()
        {
            // Arrange
            CacheServiceSetup.SetupGetStringReturnsNull();
            CacheServiceSetup.SetupSetStringByReturnsCompletedTask();

            // Act
            var result = await _sut.GetDebts(ExistedStudentsUuids.FirstGuid);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterOrEqualTo(2);
            result.Any(x => 50 < x.Borrowed).Should().BeTrue();
        }

        [Fact]
        public async Task GetDebtsByStudentId_WithData_ReturnsIEnumerableOfDebtResponseFromCache()
        {
            // Arrange

            CacheServiceSetup.SetupGetStringReturnsData();
            CacheServiceSetup.SetupSetStringByReturnsCompletedTask();

            // Act
            var result = await _sut.GetDebts(ExistedStudentsUuids.FirstGuid);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterOrEqualTo(2);
            result.Any(x => 50 < x.Borrowed).Should().BeTrue();
        }

        [Fact]
        public async Task CreateDebt_WithData_ReturnsCreateDebtResponse()
        {
            // Arrange
            CacheServiceSetup.SetupClearStorage();

            var model = _debtsDataHelper.GenerateCreateDebtModel();

            // Act
            var result = await _sut.CreateDebt(model);

            // Assert
            result.Should().NotBeNull();
            result.StudentId.Should().Be(model.StudentId);
        }

        [Fact]
        public async Task UpdateDebt_WithData_ReturnsUpdateDebtResponse()
        {
            // Arrange
            CacheServiceSetup.SetupClearStorage();

            var id = 1;
            var model = _debtsDataHelper.GenerateUpdateDebtModel();

            // Act
            var result = await _sut.UpdateDebt(id, model);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteDebt_WithData_ReturnsDeleteDebtResponse()
        {
            // Arrange
            CacheServiceSetup.SetupClearStorage();
            var id = 2;

            // Act
            var result = await _sut.DeleteDebt(id);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetUrgentlyPaidDebts_WithData_ReturnsIEnumerableOfDebtResponse()
        {
            // Arrange
            CacheServiceSetup.SetupGetStringReturnsNull();

            // Act
            var result = await _sut.GetUrgentlyRepaidDebts(ExistedStudentsUuids.FirstGuid);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetUrgentlyPaidDebts_WithData_ReturnsIEnumerableOfDebtResponseFromCache()
        {
            // Arrange
            CacheServiceSetup.SetupGetStringReturnsData();

            // Act
            var result = await _sut.GetUrgentlyRepaidDebts(ExistedStudentsUuids.FirstGuid);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetOverdueDebts_WithData_ReturnsIEnumerableOfDebtResponse()
        {
            // Arrange
            CacheServiceSetup.SetupGetStringReturnsData();

            // Act
            var result = await _sut.GetOverdueDebts(ExistedStudentsUuids.FirstGuid);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterOrEqualTo(1);
            result.ToList()[0].Borrowed.Should().BeGreaterOrEqualTo(50);
        }
    }
}
