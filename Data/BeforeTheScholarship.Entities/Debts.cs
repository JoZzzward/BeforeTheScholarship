namespace BeforeTheScholarship.Entities;

/// <summary>
/// Debt contain information about debt and foreign key to StudentUser model. 
/// </summary>
public class Debts : BaseEntity
{
    public Guid StudentId { get; set; }
    public StudentUser StudentUser { get; set; }
    public decimal Borrowed { get; set; }
    public string? Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTimeOffset WhenBorrowed { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset WhenToPayback { get; set; } = DateTimeOffset.UtcNow.AddDays(3);
}
