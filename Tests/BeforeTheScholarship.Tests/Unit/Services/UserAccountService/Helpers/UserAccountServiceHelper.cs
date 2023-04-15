using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers.ValidationHelpers;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.SignInManager;
using BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers.Identity.UserManger;

namespace BeforeTheScholarship.Tests.Unit.Services.UserAccountService.Helpers
{
    public class UserAccountServiceHelper
    {
        private readonly ValidationSetup _validationSetup = new();

        public IUserAccountService Initialize()
        {
            var service = new BeforeTheScholarship.Services.UserAccountService.UserAccountService(
                    AutoMapperInitializer.Initialize(),
                    UserManagerInitializer.Initialize(),
                    SignInManagerInitializer.Initialize(),
                    Substitute.For<IEmailSender>(),
                    LoggerInitializer.InitializeForType<BeforeTheScholarship.Services.UserAccountService.UserAccountService>(),
                    _validationSetup.SetupRegisterUserAccountModel(),
                    _validationSetup.SetupSendConfirmationEmailModel(),
                    _validationSetup.SetupLoginUserAccountModel(),
                    _validationSetup.SetupConfirmationEmailModel(),
                    _validationSetup.SetupSendPasswordRecoveryModel(),
                    _validationSetup.SetupPasswordRecoveryModel(),
                    _validationSetup.SetupChangePasswordModel()
                );

            return service;
        }
    }
}
