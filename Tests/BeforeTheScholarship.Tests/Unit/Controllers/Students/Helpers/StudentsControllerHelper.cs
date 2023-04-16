using BeforeTheScholarship.Api.Controllers.Students;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.StudentService.Models;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Students.Helpers
{
    public static class StudentsControllerHelper
    {
        private static IStudentService _studentService;

        public static IStudentService Initialize()
        {
            _studentService = Substitute.For<IStudentService>();

            return _studentService;
        }

        public static class Setup
        {
            private static readonly StudentsDataHelper _studentsDataHelper = new();

            public static void SetupGetAllStudentsReturnsData()
            {
                var content = _studentsDataHelper.GenerateStudentResponses();
                var contentList = content.ToList();
                _studentService.GetStudents().Returns(content);
            }


            public static void SetupGetStudentByIdReturnsData()
            {
                var id = ExistedStudentConsts.Id;

                var response = new StudentResponse
                {
                    Id = id,
                    FirstName = Guid.NewGuid().Shrink().Divide(4),
                    LastName = Guid.NewGuid().Shrink().Divide(4),
                    UserName = ExistedStudentConsts.UserName,
                    Email = ExistedStudentConsts.Email,
                    EmailConfirmed = false,
                    PhoneNumber = "1234567" + new Random().Next(1000, 9999)
                };

                _studentService.GetStudentById(id).Returns(response);
            }

            public static void SetupUpdateStudentReturnsData()
            {
                var response = new UpdateStudentResponse
                {
                    Id = ExistedStudentConsts.Id
                };

                _studentService.UpdateStudent(Arg.Any<Guid>(), Arg.Any<UpdateStudentModel>()).Returns(response);
            }

            public static void SetupDeleteStudentReturnsData()
            {
                var response = new DeleteStudentResponse
                {
                    Id = ExistedStudentConsts.Id
                };

                _studentService.DeleteStudent(Arg.Any<Guid>()).Returns(response);
            }
        }
    }
}
