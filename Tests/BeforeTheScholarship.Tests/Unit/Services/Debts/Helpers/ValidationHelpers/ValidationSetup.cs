using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Services.DebtService;

namespace BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers.ValidationHelpers
{
    public partial class ValidationSetup
    {
        public IModelValidator<CreateDebtModel> SetupCreateDebtModelValidatorReturnsTrue()
        {
            var validator = Substitute.For<IModelValidator<CreateDebtModel>>();
            validator.CheckValidation(Arg.Any<CreateDebtModel>()).Returns(true);

            return validator;
        }

        public IModelValidator<UpdateDebtModel> SetupUpdateDebtModelValidatorReturnsTrue()
        {
            var validator = Substitute.For<IModelValidator<UpdateDebtModel>>();
            validator.CheckValidation(Arg.Any<UpdateDebtModel>()).Returns(true);

            return validator;
        }
    }
}
