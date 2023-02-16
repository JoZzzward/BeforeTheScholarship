namespace BeforeTheScholarship.Entities;

/// <summary>
/// Debt contain information about debt and foreign key to StudentUser model. 
/// </summary>
public class Debts : BaseEntity
{
    public int StudentId { get; set; }
    public StudentUser StudentUser { get; set; }
    public decimal Borrowed { get; set; }
    public string Phone { get; set; } = "";
    public string BorrowedFromWho { get; set; }
    public DateTime WhenBorrowed { get; set; } = DateTime.UtcNow.Date;
    public DateTime WhenToPayback { get; set; } = DateTime.UtcNow.Date.AddDays(1);
}
