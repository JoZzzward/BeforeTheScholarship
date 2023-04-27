namespace BeforeTheScholarship.Web.Pages.Auth;

using Models;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<LoginResult> Login(LoginUserAccountRequest loginModel);
    Task<RegisterUserAccountResponse> Register(RegisterUserAccountRequest registerModel);
    Task<SendConfirmationEmailResponse> SendConfirmEmail(SendConfirmationEmailRequest model);
    Task<ConfirmationEmailResponse> ConfirmEmail(ConfirmationEmailRequest confirmationEmail);
    Task<PasswordRecoveryResponse> RecoverPassword(PasswordRecoveryRequest passwordRecoveryRequest);
    Task<PasswordRecoveryResponse> SendRecoverPasswordMail(SendPasswordRecoveryRequest sendPasswordRecoveryRequest);
    Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest changePasswordRequest);
    Task Logout();
}
