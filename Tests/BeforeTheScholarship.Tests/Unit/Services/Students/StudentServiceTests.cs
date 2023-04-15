using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.StudentService.Models;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using BeforeTheScholarship.Tests.Unit.Services.Students.Helpers;
using FluentAssertions;

namespace BeforeTheScholarship.Tests.Unit.Services.Students
{
    public class StudentServiceTests
    {
        private readonly IStudentService _studentService;
        private readonly StudentServiceHelper _studentServiceHelper = new();

        public StudentServiceTests()
        {
            _studentService = _studentServiceHelper.InitializeStudentService();
        }

        [Fact]
        public async Task GetStudents_WithData_ReturnsIEnumerableOfStudentResponse()
        {
            // Arrange

            // Act
            var result = await _studentService.GetStudents();
            
            // Assert
            result.Count().Should().BeGreaterOrEqualTo(2);
            result.ToList().Any(x => !string.IsNullOrEmpty(x.Email)).Should().BeTrue();
        }

        [Fact]
        public async Task GetStudentById_WithData_ReturnsIEnumerableOfStudentResponse()
        {
            // Arrange

            // Act
            var result = await _studentService.GetStudentById(ExistedStudentsUuids.FirstGuid);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task UpdateStudent_WithData_ReturnsIEnumerableOfStudentResponse()
        {
            // Arrange
            var id = ExistedStudentsUuids.FirstGuid;

            // Act
            var result = await _studentService.UpdateStudent(id, new UpdateStudentModel());

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteStudent_WithData_ReturnsIEnumerableOfStudentResponse()
        {
            // Arrange
            var id = ExistedStudentsUuids.FirstGuid;

            // Act
            var result = await _studentService.DeleteStudent(id);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
