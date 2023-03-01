namespace BeforeTheScholarship.UserAccountService.Models;

public class PasswordRecoveryModel
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set;}
}