using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using Microsoft.AspNetCore.Identity;

namespace BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers
{
    public class UserAccountServiceDataHelper
    {
        public StudentUser GenerateStudentUserMoqModel()
        {
            var student = Substitute.For<StudentUser>();
            student.Id = StudentConsts.Id;
            student.UserName = StudentConsts.UserName;
            student.Email = StudentConsts.Email;
            student.PasswordHash = new PasswordHasher<StudentUser>().HashPassword(student, StudentConsts.Password);

            return student;
        }
    }
}
