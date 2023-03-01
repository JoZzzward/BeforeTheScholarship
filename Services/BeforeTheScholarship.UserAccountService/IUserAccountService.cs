
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
    /// Registers new user. Sends mail with ConfirmationLink on user email if it was provided.
    /// </summary>
    /// <returns></returns>
    Task<UserAccountModel> RegisterUser(RegisterUserAccountModel model);

    /// <summary>
    /// Finding user by email. Generates token for password resetting and recovers password with given new one.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PasswordRecoveryResponse> RecoverPassword(PasswordRecoveryModel request);

    /// <summary>
    /// Sending mail on email that contain link with user email and token for password recovery.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PasswordRecoveryResponse> SendRecoveryPasswordEmail(SendPasswordRecoveryModel request);

    /// <summary>
    /// Changing the password of the user who specified the email address. Checks the old password for correctness.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ChangePasswordResponse> ChangePassword(ChangePasswordModel request);

}