using BeforeTheScholarship.Web.Pages.Debts.Models;

namespace BeforeTheScholarship.Web.Pages.Debts.Services
{
    public interface IDebtService
    {
        Task<IEnumerable<DebtListItem>?> GetDebtsByStudentIdAsync(string additionalUrl = "");
        Task<T> SendPostQuery<T>(string url, object data);
        Task<T> SendPutQuery<T>(string url, object data);
        Task<T> SendDeleteQuery<T>(string url);
    }
}
