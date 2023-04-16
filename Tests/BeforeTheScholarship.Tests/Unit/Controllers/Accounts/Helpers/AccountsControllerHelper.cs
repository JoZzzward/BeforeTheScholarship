using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Services.UserAccountService.Models;
using BeforeTheScholarship.Tests.Integration.Base.Data;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Accounts.Helpers
{
    public static class AccountsControllerHelper
    {
        private static IUserAccountService _userAccountService;

        public static IUserAccountService Initialize()
        {
            _userAccountService = Substitute.For<IUserAccountService>();

            return _userAccountService;
        }

        public static class Setup
        {
            public static void SetupRegisterUserReturnsData()
            {
                var response = new RegisterUserAccountResponse
                {
                    Email = StudentConsts.Email,
                    UserId = StudentConsts.Id.ToString()
                };

                _userAccountService.RegisterUser(Arg.Any<RegisterUserAccountModel>()).Returns(response);
            }

            public static void SetupLoginUserReturnsData()
            {
                var response = new LoginUserAccountResponse
                {
                    Email = StudentConsts.Email,
                };

                _userAccountService.LoginUser(Arg.Any<LoginUserAccountModel>()).Returns(response);
            }

            public static void SetupSendConfirmEmailReturnsData()
            {
                var response = new SendConfirmationEmailResponse
                {
                    Email = StudentConsts.Email,
                };

                _userAccountService.SendConfirmEmail(Arg.Any<SendConfirmationEmailModel>()).Returns(response);
            }

            public static void SetupConfirmEmailReturnsData()
            {
                var response = new ConfirmationEmailResponse
                {
                    Email = StudentConsts.Email,
                };

                _userAccountService.ConfirmEmail(Arg.Any<ConfirmationEmailModel>()).Returns(response);
            }

            public static void SetupSendRecoveryPasswordEmailReturnsData()
            {
                var response = new PasswordRecoveryResponse
                {
                    Email = StudentConsts.Email,
                };

                _userAccountService.SendRecoveryPasswordEmail(Arg.Any<SendPasswordRecoveryModel>()).Returns(response);
            }

            public static void SetupRecoverPasswordReturnsData()
            {
                var response = new PasswordRecoveryResponse
                {
                    Email = StudentConsts.Email
                };

                _userAccountService.RecoverPassword(Arg.Any<PasswordRecoveryModel>()).Returns(response);
            }

            public static void SetupChangePasswordReturnsData()
            {
                var response = new ChangePasswordResponse
                {
                    Email = StudentConsts.Email,
                };

                _userAccountService.ChangePassword(Arg.Any<ChangePasswordModel>()).Returns(response);
            }
        }
    }
}
