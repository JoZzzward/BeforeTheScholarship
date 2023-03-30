using BeforeTheScholarship.Services.StudentService.Models;
using BeforeTheScholarship.Tests.Unit.Helpers.Data;


namespace BeforeTheScholarship.Tests.Unit.Controllers.Students.Helpers
{
    public class StudentsDataHelper
    {
        private readonly DbContextHelper _dbContextHelper = new();

        public IEnumerable<StudentResponse> GenerateStudentResponses()
        {
            using var context = _dbContextHelper.GetContextData();

            var content = new List<StudentResponse>();

            foreach (var item in context.StudentUsers)
            {
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
                content.Add(new StudentResponse
                {
                    Id = Guid.NewGuid(),
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    UserName = item.UserName,
                    Email = item.Email,
                    EmailConfirmed = item.EmailConfirmed,
                    PhoneNumber = item.PhoneNumber
                });
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
            }

            return content;
        }
    }
}
