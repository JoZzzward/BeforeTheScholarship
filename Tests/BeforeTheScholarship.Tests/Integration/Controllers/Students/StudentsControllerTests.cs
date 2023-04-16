using BeforeTheScholarship.Api.Controllers.Students;
using BeforeTheScholarship.Services.StudentService.Models;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Integration.Controllers.Students.Helpers;
using BeforeTheScholarship.Tests.Integration.Core;
using FluentAssertions;

namespace BeforeTheScholarship.Tests.Integration.Controllers.Students
{
    [Trait("Category", "Integration")]
    public class StudentsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly StudentsControllerDataHelper _sutDataHelper = new();
        public StudentsControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.SetupClient();
        }

        [Fact]
        public async Task GetStudents_WithData_Returns200Response()
        {
            // Arrange
            var url = "students";

            // Act
            var response = await _client.GetAsync(url);
            var content = _sutDataHelper.GenerateContentFromModel<IEnumerable<StudentResponse>>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Count().Should().BeGreaterOrEqualTo(1);
            content.All(x => x.Email.Length > 3).Should().BeTrue();
        }

        [Fact]
        public async Task GetStudentById_WithData_Returns200Response()
        {
            // Arrange
            var url = $"students/{StudentConsts.Id}";

            // Act
            var response = await _client.GetAsync(url);
            var content = _sutDataHelper.GenerateContentFromModel<StudentResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Should().NotBeNull();
            content.Email.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateStudent_WithData_Returns200Response()
        {
            // Arrange
            var url = $"students/{StudentConsts.Id}";

            var model = new UpdateStudentRequest
            {
                Email = StudentConsts.Email,
                FirstName = "lname",
                LastName = "fname",
                PhoneNumber = "12345678901",
                UserName = StudentConsts.UserName
            };

            var request = _sutDataHelper.GenerateRequestFromModel(model);

            // Act
            var response = await _client.PutAsync(url, request);
            var content = _sutDataHelper.GenerateContentFromModel<UpdateStudentResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Should().NotBeNull();
            content.Id.Should().Be(StudentConsts.Id);
        }

        [Fact]
        public async Task DeleteStudent_WithData_Returns200Response()
        {
            // Arrange
            var url = $"students/{StudentConsts.Id}";

            // Act
            var response = await _client.DeleteAsync(url);
            var content = _sutDataHelper.GenerateContentFromModel<DeleteStudentResponse>(response);

            // Asserts
            response.EnsureSuccessStatusCode();
            content.Should().NotBeNull();
            content.Id.Should().Be(StudentConsts.Id);
        }
    }
}
