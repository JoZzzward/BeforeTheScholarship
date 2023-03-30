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

            var content = new List<DebtResponse>();

            foreach (var item in context.Debts)
            {
                if (studentId == null || item.StudentId == studentId)
                {
#pragma warning disable CS8601 // Possible null reference assignment.
                    content.Add(new DebtResponse()
                    {
                        Borrowed = item.Borrowed,
                        BorrowedFromWho = item.BorrowedFromWho,
                        WhenBorrowed = item.WhenBorrowed,
                        WhenToPayback = item.WhenToPayback,
                        Phone = item.Phone,
                        Id = item.Id
                    });
#pragma warning restore CS8601 // Possible null reference assignment.
                }
            }

            return content;
        }
    }
}
