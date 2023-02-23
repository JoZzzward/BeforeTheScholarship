using BeforeTheScholarship.Services.UserAccount.Models;

namespace BeforeTheScholarship.Services.UserAccount;

public interface IUserAccountService
{
    /// <summary>
    /// Confirms user email 
    /// </summary>
    /// <param name="confirmationEmail">Contains email that need to confirm and token for cor</param>
    /// <returns></returns>
    Task ConfirmEmail(ConfirmationEmail confirmationEmail);

    /// <summary>
    /// Create user account
    /// </summary>
    /// <returns></returns>
    Task<UserAccountModel> Create(RegisterUserAccountModel model);

}