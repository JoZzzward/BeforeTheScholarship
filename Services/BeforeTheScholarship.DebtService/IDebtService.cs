using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.DebtService;

public interface IDebtService
{
    /// <summary>
    /// Returns a list of Debts
    /// </summary>
    Task<IEnumerable<DebtModel>> GetDebts();
    /// <summary>
    /// Returns a list of Debts for Student with <paramref name="studentId"/>
    /// </summary>
    Task<IEnumerable<DebtModel>> GetDebts(Guid? studentId);
    /// <summary>
    /// Adds a new Debt to the database
    /// </summary>
    Task<DebtModel> CreateDebt(AddDebtModel model);
    /// <summary>
    /// Updates a <see cref="DebtModel"/> in database with the same <paramref name="id"/>
    /// </summary>
    Task UpdateDebt(int? id, UpdateDebtModel model);
    /// <summary>
    /// Removes a <see cref="DebtModel"/> in database with the same <paramref name="id"/>.
    /// </summary>
    Task DeleteDebt(int? id);

    /// <summary>
    /// Returns debts that need to be urgently repaid
    /// </summary>
    Task<IEnumerable<DebtModel>> GetUrgentlyRepaidDebts(Guid studentId, bool overdue);
}