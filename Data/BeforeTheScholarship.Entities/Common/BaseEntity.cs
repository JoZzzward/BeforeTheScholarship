using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Entities;

[Index("Uid", IsUnique = true)]
public class BaseEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid Uid { get; set; } = Guid.NewGuid();
}
