using AutoMapper;
using BeforeTheScholarship.Api.Controllers.Students;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.StudentService.Models;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Students
{
    public class StudentsControllerTests
    {
        private readonly StudentsController _controller;

        private readonly IStudentService _studentService = Substitute.For<IStudentService>();
        private readonly ILogger<StudentsController> _logger = Substitute.For<ILogger<StudentsController>>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();

        private readonly StudentsDataHelper _studentsDataHelper = new();

        public StudentsControllerTests()
        {
            _controller = new StudentsController(_studentService, _logger, _mapper);
        }

        [Fact]
        public async Task GetStudents_WithoutData_IEnumerableOfStudentResponse()
        {
            // Arrange
            var content = _studentsDataHelper.GenerateStudentResponses();

            _studentService.GetStudents().Returns(content);
            // Act 
            var result = await _controller.GetStudents();

            // Assert
            result.Count().Should().BeGreaterOrEqualTo(2);
        }

        [Fact]
        public async Task GetStudentById_WithStudentId_ReturnsStudentResponse()
        {
            // Arrange
            var id = ExistedStudentsUuids.FirstGuid;

            var response = new StudentResponse
            {
                Id = id,
                FirstName = Guid.NewGuid().Shrink().Divide(4),
                LastName = Guid.NewGuid().Shrink().Divide(4),
                UserName = Guid.NewGuid().Shrink().Divide(4),
                Email = Guid.NewGuid().Shrink().Divide(4) + "@email.com",
                EmailConfirmed = false,
                PhoneNumber = "1234567" + new Random().Next(1000, 9999)
            };

            _studentService.GetStudentById(id).Returns(response);

            // Act 
            var result = await _controller.GetStudentById(id);

            // Assert
            result.FirstName.Should().Be(response.FirstName);
            result.UserName.Should().Be(response.UserName);
        }

        [Fact]
        public async Task UpdateStudent_WithoutData_ReturnsOkResult()
        {
            // Arrange
            var model = new UpdateStudentModel();

            var request = new UpdateStudentRequest();

            var response = new UpdateStudentResponse();

            _studentService.UpdateStudent(Arg.Any<Guid>(), model).Returns(response);

            // Act 
            var result = await _controller.UpdateStudent(Guid.NewGuid(), request);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteStudent_WithoutData_ReturnsOkResult()
        {
            // Arrange
            var response = new DeleteStudentResponse();

            _studentService.DeleteStudent(Arg.Any<Guid>()).Returns(response);

            // Act 
            var result = await _controller.DeleteStudent(Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkResult>();
        }
    }
}
