using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using BeforeTheScholarship.Tests.Unit.Helpers.Data;
using BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers.ValidationHelpers;

namespace BeforeTheScholarship.Tests.Unit.Services.Students.Helpers
{
    public class StudentServiceHelper
    {
        private readonly ValidationSetup _validationSetup = new();

        public IStudentService InitializeStudentService()
        {
            var studentService = new StudentService
            (
                new AppDbContextFactory(),
                AutoMapperInitializer.Initialize(),
                LoggerInitializer.InitializeForType<StudentService>(),
                _validationSetup.SetupUpdateStudentModel()
            );

            return studentService;
        }
    }
}
