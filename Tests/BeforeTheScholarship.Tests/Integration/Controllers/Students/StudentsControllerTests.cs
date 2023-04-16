using BeforeTheScholarship.Tests.Integration.Core;

namespace BeforeTheScholarship.Tests.Integration.Controllers.Students
{
    public class StudentsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public StudentsControllerTests(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.SetupClient();


        }

    }
}
