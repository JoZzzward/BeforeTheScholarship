using Microsoft.AspNetCore.Identity;

namespace BeforeTheScholarship.Entities;

/// <summary>
/// Main model that users will user after registration
/// </summary>

public class StudentUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Debts> Debts { get; set; }
}
