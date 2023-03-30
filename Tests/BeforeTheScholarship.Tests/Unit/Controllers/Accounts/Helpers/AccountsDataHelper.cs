using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.StudentService.Models;
using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Services.UserAccountService.Models;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using BeforeTheScholarship.Tests.Unit.Helpers.Data;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Accounts.Helpers
{
    public class AccountsDataHelper
    {
        private readonly DbContextHelper _dbContextHelper = new();

        public UserAccountService CreateUserAccountService()
        {
            var registerModelValidator = Substitute.For<IModelValidator<RegisterUserAccountModel>>();
            registerModelValidator.CheckValidation(Arg.Any<RegisterUserAccountModel>()).Returns(true);

            var loginModelValidator = Substitute.For<IModelValidator<LoginUserAccountModel>>();
            loginModelValidator.CheckValidation(Arg.Any<LoginUserAccountModel>()).Returns(true);

            var confirmationEmailModelValidator = Substitute.For<IModelValidator<ConfirmationEmailModel>>();
            confirmationEmailModelValidator.CheckValidation(Arg.Any<ConfirmationEmailModel>()).Returns(true);

            var sendPasswordRecoveryModelValidator = Substitute.For<IModelValidator<PasswordRecoveryMailModel>>();
            sendPasswordRecoveryModelValidator.CheckValidation(Arg.Any<PasswordRecoveryMailModel>()).Returns(true);

            var passwordRecoveryModelValidator = Substitute.For<IModelValidator<PasswordRecoveryModel>>();
            passwordRecoveryModelValidator.CheckValidation(Arg.Any<PasswordRecoveryModel>()).Returns(true);

            var changePasswordModelValidator = Substitute.For<IModelValidator<ChangePasswordModel>>();
            changePasswordModelValidator.CheckValidation(Arg.Any<ChangePasswordModel>()).Returns(true);

            var service = new UserAccountService(
                AutoMapperInitializer.Initialize(),
                UserManagerInitializer.Initialize(),
                SignInManagerInitializer.Initialize(),
                Substitute.For<IEmailSender>(),
                LoggerInitializer.Initialize<UserAccountService>(),
                registerModelValidator,
                loginModelValidator,
                confirmationEmailModelValidator,
                sendPasswordRecoveryModelValidator,
                passwordRecoveryModelValidator,
                changePasswordModelValidator);

            return service;
        }

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
                })
                .ToList();
        }
    }
}
