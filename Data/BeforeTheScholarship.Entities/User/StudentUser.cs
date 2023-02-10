using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Entities.User;

[Index("Uid", IsUnique = true, Name = "Uid_Index")]
public class StudentUser : IdentityUser<Guid>
{
    public Guid Uid { get; set; } = Guid.NewGuid();

    public ICollection<Debts> Debts { get; set; }
}
