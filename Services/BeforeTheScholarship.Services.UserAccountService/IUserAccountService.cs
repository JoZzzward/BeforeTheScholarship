using BeforeTheScholarship.Services.UserAccountService.Models;

namespace BeforeTheScholarship.Services.UserAccountService;

public interface IUserAccountService
{
    /// <summary>
    /// Confirms user email 
    /// </summary>
    /// <param name="confirmationEmail">Contains a email that need to confirm by given token</param>
    Task<ConfirmationEmailResponse> ConfirmEmail(ConfirmationEmailModel confirmationEmail);

    /// <summary>
    /// Registers new user. Sends mail with ConfirmationLink on user email if it was provided.
    /// </summary>
    /// <param name="request">Contains username, email and password of user that want to register</param>
    Task<RegisterUserAccountResponse> RegisterUser(RegisterUserAccountModel request);

    /// <summary>
    /// Sign in user with given Email
    /// </summary>
    /// <param name="request">Contains user email and password that want to sign in</param>
    Task<LoginUserAccountResponse> LoginUser(LoginUserAccountModel request);

    /// <summary>
    /// Finding user by email. Generates token for password resetting and recovers password with given new one.
    /// </summary>
    /// <param name="request">Contains the email of the user who wants to recover the password</param>
    Task<PasswordRecoveryResponse> RecoverPassword(PasswordRecoveryModel request);

    /// <summary>
    /// Sending mail on email that contain link with user email and token for password recovery.
    /// </summary>
    /// <param name="request">Contains the email of the user who wants to recover the password</param>
    Task<PasswordRecoveryResponse> SendRecoveryPasswordEmail(SendPasswordRecoveryModel request);

    /// <summary>
    /// Changing the password of the user who specified the email address. Checks the old password for correctness.
    /// </summary>
    /// <param name="request">Contains the email of the user who wants to change the old password by new password</param>
    Task<ChangePasswordResponse> ChangePassword(ChangePasswordModel request);

}