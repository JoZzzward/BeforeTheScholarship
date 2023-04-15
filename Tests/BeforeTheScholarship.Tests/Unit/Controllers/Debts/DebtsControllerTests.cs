using AutoMapper;
using BeforeTheScholarship.Api.Controllers.Debts;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.DebtService.Models;
using BeforeTheScholarship.Tests.Unit.Base.Services.Debts;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Debts
{
    [Trait("Category", "Unit")]
    public class DebtsControllerTests
    {
        private readonly DebtsController _controller;

        private readonly IDebtService _debtService = Substitute.For<IDebtService>();
        private readonly ILogger<DebtsController> _logger = LoggerInitializer.InitializeForType<DebtsController>();
        private readonly IMapper _mapper = AutoMapperInitializer.Initialize();

        private readonly DebtsDataHelper _debtsDataHelper = new();

        public DebtsControllerTests()
        {
            _controller = new DebtsController(_debtService, _logger, _mapper);
        }

        [Fact]
        public async Task GetDebts_WithData_ReturnsIEnumerableOfDebtResponse()
        {
            // Arrange
            var content = _debtsDataHelper.GenerateDebtResponses();

            _debtService.GetDebts().Returns(content);
            // Act 
            var result = await _controller.GetDebts();

            // Assert
            result.Count().Should().BeGreaterOrEqualTo(3);
        }

        [Fact]
        public async Task GetDebts_WithStudentId_ReturnsIEnumerableOfDebtResponse()
        {
            // Arrange
            Guid? studentId = ExistedStudentsUuids.FirstGuid;
            var content = _debtsDataHelper.GenerateDebtResponses(studentId);

            _debtService.GetDebts(studentId).Returns(content);

            // Act 
            var result = await _controller.GetDebts(studentId);

            // Assert
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task CreateDebt_WithoutData_ReturnsCreateDebtResponse()
        {
            // Arrange
            var model = new CreateDebtModel();

            var response = new CreateDebtResponse();

            var request = new CreateDebtRequest();

            _debtService.CreateDebt(model).Returns(response);

            // Act 
            var result = await _controller.CreateDebt(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo(typeof(CreateDebtResponse));
        }

        [Fact]
        public async Task UpdateDebt_WithData_ReturnsOkObjectResult()
        {
            // Arrange
            var request = new UpdateDebtRequest
            {
                Borrowed = new Random().Next(50, 250),
                Phone = "11111111111",
                BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                WhenToPayback = DateTimeOffset.Now.AddDays(10)
            };

            var model = new UpdateDebtModel
            {
                Borrowed = request.Borrowed,
                Phone = request.Phone,
                BorrowedFromWho = request.BorrowedFromWho,
                WhenToPayback = request.WhenToPayback
            };

            var response = new UpdateDebtResponse();

            _debtService.UpdateDebt(Arg.Any<int>(), model).Returns(response);

            // Act 
            var result = await _controller.UpdateDebt(1, request);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task DeleteDebt_WithoutData_ReturnsOkObjectResult()
        {
            // Assert
            var response = new DeleteDebtResponse();

            _debtService.DeleteDebt(Arg.Any<int>()).Returns(response);

            // Act 
            var result = await _controller.DeleteDebt(1);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetUrgentlyRepaidDebts_WithData_ReturnsIEnumerableOfDebtResponses()
        {
            // Arrange
            Guid studentId = ExistedStudentsUuids.FirstGuid;
            var content = _debtsDataHelper.GenerateDebtResponses(studentId);
            content = content.ToList()
                            .Where(x => x.WhenToPayback > DateTimeOffset.UtcNow &&
                            x.WhenToPayback <= DateTimeOffset.UtcNow.AddDays(1));
            _debtService.GetUrgentlyRepaidDebts(studentId).Returns(content);
            // Act 
            var result = await _controller.GetUrgentlyRepaidDebts(studentId);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetOverdueDebts_WithData_ReturnsNull()
        {
            // Arrange
            Guid studentId = ExistedStudentsUuids.FirstGuid;
            var content = _debtsDataHelper.GenerateDebtResponses(studentId);
            content = content.ToList().Where(x => (x.WhenToPayback - DateTimeOffset.Now).TotalDays <= 0);

            _debtService.GetOverdueDebts(studentId).Returns(content);

            // Act 
            var result = await _controller.GetOverdueDebts(studentId);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(0);
        }
    }
}