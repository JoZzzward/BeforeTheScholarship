namespace BeforeTheScholarship.Web.Pages.Profile.Models;

public class StudentUserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
}
