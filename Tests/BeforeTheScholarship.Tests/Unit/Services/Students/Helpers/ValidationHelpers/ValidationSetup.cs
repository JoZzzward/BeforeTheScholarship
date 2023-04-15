using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Services.StudentService.Models;

namespace BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers.ValidationHelpers
{
    public partial class ValidationSetup
    {
        public IModelValidator<UpdateStudentModel> SetupUpdateStudentModel()
        {
            var validator = Substitute.For<IModelValidator<UpdateStudentModel>>();
            validator.CheckValidation(Arg.Any<UpdateStudentModel>()).Returns(true);

            return validator;
        }
    }
}
