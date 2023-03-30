using BeforeTheScholarship.Services.DebtService.Models;

namespace BeforeTheScholarship.Services.DebtService;

public interface IDebtService
{
    /// <summary>
    /// Returns a list of Debts
    /// </summary>
    Task<IEnumerable<DebtResponse>> GetDebts();
    /// <summary>
    /// Returns a list of Debts for Student with <paramref name="studentId"/>
    /// </summary>
    Task<IEnumerable<DebtResponse>> GetDebts(Guid? studentId);
    /// <summary>
    /// Adds a new Debt to the database
    /// </summary>
    Task<CreateDebtResponse> CreateDebt(CreateDebtModel model);
    /// <summary>
    /// Updates a <see cref="DebtResponse"/> in database with the same <paramref name="id"/>
    /// </summary>
    Task<UpdateDebtResponse?> UpdateDebt(int? id, UpdateDebtModel model);
    /// <summary>
    /// Removes a <see cref="DebtResponse"/> in database with the same <paramref name="id"/>.
    /// </summary>
    Task<DeleteDebtResponse?> DeleteDebt(int? id);
    /// <summary>
    /// Returns debts with 1 day or less left to the repayment date and that need to be urgently repaid
    /// </summary>
    Task<IEnumerable<DebtResponse>> GetUrgentlyRepaidDebts(Guid? studentId);
    /// <summary>
    /// Returns debts that must be overdue
    /// </summary>
    Task<IEnumerable<DebtResponse>> GetOverdueDebts(Guid? studentId);
}