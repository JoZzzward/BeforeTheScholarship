using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.DebtService;

public interface IDebtService
{
    /// <summary>
    /// Returns a list of Debts
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<DebtModel>> GetDebts(int? studentId);
    /// <summary>
    /// Returns <see cref="DebtModel"/> with same <paramref name="id"/>
    /// </summary>
    Task<DebtModel> GetDebtById(int? id);
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
    /// 
    /// </summary>
    Task<IEnumerable<DebtModel>> GetUrgentlyRepaidDebts(int studentId, bool overdue);
}