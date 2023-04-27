namespace BeforeTheScholarship.Web.Pages.Auth.Models;
public class RegistrationResult
{
    public bool Success { get; set; }
    public string UserName { get; set; }
    public string Error { get; set; }
    public string ErrorDescription { get; set; }
}
