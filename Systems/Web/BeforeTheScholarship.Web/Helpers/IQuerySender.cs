using BeforeTheScholarship.Web.Pages.Debts.Models;

namespace BeforeTheScholarship.Web.Helpers
{
    public interface IQuerySender
    {
        Task<IEnumerable<DebtListItem>?> SendGetQuery(string url);
    }
}
