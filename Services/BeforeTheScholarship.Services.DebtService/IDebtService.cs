using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.DebtService.Models;

namespace BeforeTheScholarship.Services.DebtService;

public interface IDebtService
{
    /// <summary>
    /// Returns a list of Debts
    /// </summary>
    Task<IEnumerable<DebtResponse>?> GetDebts();
    /// <summary>
    /// Returns a list of Debts for Student with <paramref name="studentId"/>
    /// </summary>
    Task<IEnumerable<DebtResponse>?> GetDebts(Guid? studentId);
    /// <summary>
    /// Adds a new Debt to the database
    /// </summary>
    Task<CreateDebtResponse?> CreateDebt(CreateDebtModel model);
    /// <summary>
    /// Updates a <see cref="DebtResponse"/> in database with the same <paramref name="id"/>
    /// </summary>
    Task<UpdateDebtResponse?> UpdateDebt(Guid? id, UpdateDebtModel model);
    /// <summary>
    /// Removes a <see cref="Debts"/> model with the same <paramref name="id"/> from database.
    /// </summary>
    Task<DeleteDebtResponse?> DeleteDebt(Guid? id);
    /// <summary>
    /// Returns debts with 1 day or less left to the repayment date and that need to be urgently repaid
    /// </summary>
    Task<IEnumerable<DebtResponse>?> GetUrgentlyRepaidDebts(Guid? studentId);
    /// <summary>
    /// Returns debts that must be overdue
    /// </summary>
    Task<IEnumerable<DebtResponse>?> GetOverdueDebts(Guid? studentId);
}