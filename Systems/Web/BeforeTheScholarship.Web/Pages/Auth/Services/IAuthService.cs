namespace BeforeTheScholarship.Web.Pages.Auth;

using System.Threading.Tasks;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task<RegisterResponse> Register(RegisterModel registerModel);
    Task ConfirmEmail(ConfirmationEmail confirmationEmail);
    Task Logout();
}
