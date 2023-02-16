﻿namespace BeforeTheScholarship.Api.Controllers.Debts;

/// <summary>
/// Debts model
/// </summary>
public class DebtModel
{
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTime WhenBorrowed { get; set; }
    public DateTime WhenToPayback { get; set; }
}
