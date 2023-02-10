using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Entities.Common;

[Index("Uid",IsUnique = true, Name = "Uid_Index")]
[Obsolete]
public class BaseEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid Uid { get; set; } = Guid.NewGuid();
}
