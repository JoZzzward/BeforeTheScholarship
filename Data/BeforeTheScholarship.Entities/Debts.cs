namespace BeforeTheScholarship.Entities;

/// <summary>
/// Every debt contain information about debt and foreign key to StudentUser model. 
/// </summary>
public class Debts : BaseEntity
{
    public int StudentId { get; set; }
    public StudentUser StudentUser { get; set; }
    public decimal Borrowed { get; set; }
    public string Phone { get; set; } = "";
    public string BorrowedFromWho { get; set; }
    public DateTimeOffset WhenBorrowed { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset WhenToPayback { get; set; }
}
