namespace BeforeTheScholarship.Web.Pages.Auth;

public class RegisterResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Error { get; set; }
    public IEnumerable<string> ErrorDescription { get; set; }

}
