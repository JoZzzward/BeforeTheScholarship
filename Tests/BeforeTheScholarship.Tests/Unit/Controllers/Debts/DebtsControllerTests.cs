using BeforeTheScholarship.Api.Controllers.Debts;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Services.DebtService.Models;
using BeforeTheScholarship.Tests.Unit.Controllers.Debts.Consts;
using BeforeTheScholarship.Tests.Unit.Controllers.Debts.Helpers;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Debts
{
    [Trait("Category", "Unit")]
    public class DebtsControllerTests
    {
        private readonly DebtsController _controller;

        public DebtsControllerTests()
        {
            _controller = new DebtsController(
                DebtsControllerHelper.Initialize(),
                LoggerInitializer.InitializeForType<DebtsController>(),
                AutoMapperInitializer.Initialize());
        }

        [Fact]
        public async Task GetDebts_WithData_ReturnsIEnumerableOfDebtResponse()
        {
            // Arrange
            DebtsControllerHelper.Setup.SetupGetDebtsReturnsData();

            // Act 
            var result = await _controller.GetDebts();

            // Assert
            result.Count().Should().BeGreaterOrEqualTo(3);
        }

        [Fact]
        public async Task GetDebts_WithStudentId_ReturnsIEnumerableOfDebtResponse()
        {
            // Arrange
            DebtsControllerHelper.Setup.SetupGetDebtsWithStudentIdReturnsData();

            // Act 
            var result = await _controller.GetDebts(ExistedStudentConsts.Id);

            // Assert
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task CreateDebt_WithoutData_ReturnsCreateDebtResponse()
        {
            // Arrange
            DebtsControllerHelper.Setup.SetupCreateDebtReturnsData();

            var request = new CreateDebtRequest
            {
                StudentId = ExistedStudentConsts.Id,
                Borrowed = new Random().Next(50, 250),
                Phone = "11111111111",
                BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4)
            };

            // Act 
            var result = await _controller.CreateDebt(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo(typeof(CreateDebtResponse));
            result.StudentId.Should().Be(request.StudentId);
        }

        [Fact]
        public async Task UpdateDebt_WithData_ReturnsOkObjectResult()
        {
            // Arrange
            DebtsControllerHelper.Setup.SetupUpdateDebtReturnsData();

            var request = new UpdateDebtRequest
            {
                Borrowed = new Random().Next(50, 250),
                Phone = "11111111111",
                BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                WhenToPayback = DateTimeOffset.Now.AddDays(10)
            };

            var id = ExistedDebtConsts.Uid;

            // Act 
            var result = await _controller.UpdateDebt(id, request);
            var content = (result.Result as OkObjectResult).Value as UpdateDebtResponse;

            // Assert
            result.Should().NotBeNull();
            content.Should().NotBeNull();
            content.Uid.Should().Be(id);

        }

        [Fact]
        public async Task DeleteDebt_WithoutData_ReturnsOkObjectResult()
        {
            // Assert
            DebtsControllerHelper.Setup.SetupDeleteDebtReturnsData();

            var id = ExistedDebtConsts.Uid;

            // Act 
            var result = await _controller.DeleteDebt(id);
            var content = (result.Result as OkObjectResult).Value as DeleteDebtResponse;
            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
            content.Uid.Should().Be(id);
        }

        [Fact]
        public async Task GetUrgentlyRepaidDebts_WithData_ReturnsIEnumerableOfDebtResponses()
        {
            // Arrange
            DebtsControllerHelper.Setup.SetupGetUrgentlyRepaidDebtsReturnsData();

            // Act 
            var result = await _controller.GetUrgentlyRepaidDebts(ExistedStudentConsts.Id);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetOverdueDebts_WithData_ReturnsEmptyData()
        {
            // Arrange
            DebtsControllerHelper.Setup.SetupGetOverdueDebtsReturnsEmptyData();

            // Act 
            var result = await _controller.GetOverdueDebts(ExistedStudentConsts.Id);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(0);
        }
    }
}