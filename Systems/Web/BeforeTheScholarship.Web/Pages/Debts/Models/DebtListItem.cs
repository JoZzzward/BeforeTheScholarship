namespace BeforeTheScholarship.Web.Pages.Debts.Models
{
    public class DebtListItem
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public decimal Borrowed { get; set; }
        public string? Phone { get; set; }
        public string BorrowedFromWho { get; set; }
        public DateTimeOffset WhenBorrowed { get; set; }
        public DateTimeOffset WhenToPayback { get; set; }
    }
}
