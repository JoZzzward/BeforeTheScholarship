using BeforeTheScholarship.Api.Controllers.Students;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Services.DebtService.Models;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.StudentService.Models;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Helpers;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Students
{
    [Trait("Category", "Unit")]
    public class StudentsControllerTests
    {
        private readonly StudentsController _controller;
        
        public StudentsControllerTests()
        {
            _controller = new StudentsController(
                StudentsControllerHelper.Initialize(),
                LoggerInitializer.InitializeForType<StudentsController>(),
                AutoMapperInitializer.Initialize());
        }

        [Fact]
        public async Task GetAllStudents_WithoutData_ReturnsIEnumerableOfStudentResponse()
        {
            // Arrange
            StudentsControllerHelper.Setup.SetupGetAllStudentsReturnsData();

            // Act 
            var result = await _controller.GetStudents();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterOrEqualTo(2);
            result.Any(x => x.Id == ExistedStudentConsts.Id);
            result.Any(x => x.Id == ExistedStudentConsts.SecondId);
        }

        [Fact]
        public async Task GetStudentById_WithStudentId_ReturnsStudentResponse()
        {
            // Arrange
            StudentsControllerHelper.Setup.SetupGetStudentByIdReturnsData();

            var id = ExistedStudentConsts.Id;

            // Act 
            var result = await _controller.GetStudentById(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.UserName.Should().Be(ExistedStudentConsts.UserName);
        }

        [Fact]
        public async Task UpdateStudent_WithoutData_ReturnsOkResult()
        {
            // Arrange
            StudentsControllerHelper.Setup.SetupUpdateStudentReturnsData();

            var request = new UpdateStudentRequest();

            var id = ExistedStudentConsts.Id;

            // Act 
            var result = await _controller.UpdateStudent(id, request);
            var content = (result.Result as OkObjectResult).Value as UpdateStudentResponse;

            // Assert
            result.Result.Should().NotBeNull();
            content!.Id.Should().Be(id);
        }

        [Fact]
        public async Task DeleteStudent_WithoutData_ReturnsOkResult()
        {
            // Arrange
            StudentsControllerHelper.Setup.SetupDeleteStudentReturnsData();

            var id = ExistedStudentConsts.Id;

            // Act 
            var result = await _controller.DeleteStudent(id);
            var content = (result.Result as OkObjectResult).Value as DeleteStudentResponse;

            // Assert
            result.Result.Should().NotBeNull();
            content!.Id.Should().Be(id);
        }
    }
}
