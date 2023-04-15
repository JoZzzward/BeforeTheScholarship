using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Services.UserAccountService.Models;

namespace BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers.ValidationHelpers
{
    public partial class ValidationSetup
    {
        public IModelValidator<RegisterUserAccountModel> SetupRegisterUserAccountModel()
        {
            var validator = Substitute.For<IModelValidator<RegisterUserAccountModel>>();
            validator.CheckValidation(Arg.Any<RegisterUserAccountModel>()).Returns(true);

            return validator;
        }

        public IModelValidator<SendConfirmationEmailModel> SetupSendConfirmationEmailModel()
        {
            var validator = Substitute.For<IModelValidator<SendConfirmationEmailModel>>();
            validator.CheckValidation(Arg.Any<SendConfirmationEmailModel>()).Returns(true);

            return validator;
        }

        public IModelValidator<LoginUserAccountModel> SetupLoginUserAccountModel()
        {
            var validator = Substitute.For<IModelValidator<LoginUserAccountModel>>();
            validator.CheckValidation(Arg.Any<LoginUserAccountModel>()).Returns(true);

            return validator;
        }

        public IModelValidator<ConfirmationEmailModel> SetupConfirmationEmailModel()
        {
            var validator = Substitute.For<IModelValidator<ConfirmationEmailModel>>();
            validator.CheckValidation(Arg.Any<ConfirmationEmailModel>()).Returns(true);

            return validator;
        }

        public IModelValidator<SendPasswordRecoveryModel> SetupSendPasswordRecoveryModel()
        {
            var validator = Substitute.For<IModelValidator<SendPasswordRecoveryModel>>();
            validator.CheckValidation(Arg.Any<SendPasswordRecoveryModel>()).Returns(true);

            return validator;
        }

        public IModelValidator<PasswordRecoveryModel> SetupPasswordRecoveryModel()
        {
            var validator = Substitute.For<IModelValidator<PasswordRecoveryModel>>();
            validator.CheckValidation(Arg.Any<PasswordRecoveryModel>()).Returns(true);

            return validator;
        }

        public IModelValidator<ChangePasswordModel> SetupChangePasswordModel()
        {
            var validator = Substitute.For<IModelValidator<ChangePasswordModel>>();
            validator.CheckValidation(Arg.Any<ChangePasswordModel>()).Returns(true);

            return validator;
        }
    }
}
