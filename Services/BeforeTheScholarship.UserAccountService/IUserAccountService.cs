
using BeforeTheScholarship.UserAccountService.Models;

namespace BeforeTheScholarship.Services.UserAccount;

public interface IUserAccountService
{
    /// <summary>
    /// Confirms user email 
    /// </summary>
    /// <param name="confirmationEmail">Contains email that need to confirm and token for cor</param>
    /// <returns></returns>
    Task ConfirmEmail(ConfirmationEmailModel confirmationEmail);

    /// <summary>
    /// Create user account
    /// </summary>
    /// <returns></returns>
    Task<UserAccountModel> RegisterUser(RegisterUserAccountModel model);

    /// <summary>
    /// Genera 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PasswordRecoveryResponse> RecoverPassword(PasswordRecoveryModel request);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PasswordRecoveryResponse> SendRecoveryPasswordEmail(SendPasswordRecoveryModel request);

}