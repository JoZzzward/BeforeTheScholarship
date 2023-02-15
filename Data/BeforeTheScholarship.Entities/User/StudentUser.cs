namespace BeforeTheScholarship.Entities;

/// <summary>
/// Main model that users will user after registration
/// </summary>
public class StudentUser : BaseEntity
{
    //TODO: Edit the model to IdentityUser<Guid> with Index attribute and Guid field
    public string UserName { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public bool EmailConfirmed { get; set; } = false;
    public string Password { get; set; }
    public ICollection<Debts> Debts { get; set; }
}
