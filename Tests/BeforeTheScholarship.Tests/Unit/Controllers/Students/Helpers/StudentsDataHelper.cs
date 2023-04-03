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

            return context.StudentUsers.Select(item => new StudentResponse
            {
                Id = Guid.NewGuid(),
                FirstName = item.FirstName,
                LastName = item.LastName,
                UserName = item.UserName,
                Email = item.Email,
                EmailConfirmed = item.EmailConfirmed,
                PhoneNumber = item.PhoneNumber
            }).ToList();
        }
    }
}
