using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Tests.Unit.Helpers.Data;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Debts.Helpers
{
    public class DebtsDataHelper
    {
        private readonly DbContextHelper _dbContextHelper = new();

        public IEnumerable<DebtResponse> GenerateDebtResponses(Guid? studentId = null)
        {
            using var context = _dbContextHelper.GetContextData();

            return (from item in context.Debts
                where studentId == null || item.StudentId == studentId
                select new DebtResponse()
                {
                    Borrowed = item.Borrowed,
                    BorrowedFromWho = item.BorrowedFromWho,
                    WhenBorrowed = item.WhenBorrowed,
                    WhenToPayback = item.WhenToPayback,
                    Phone = item.Phone,
                    Id = item.Id
                }).ToList();
        }
    }
}
