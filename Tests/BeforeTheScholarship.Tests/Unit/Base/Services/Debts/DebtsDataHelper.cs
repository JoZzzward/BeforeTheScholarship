using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using BeforeTheScholarship.Tests.Unit.Helpers.Data;

namespace BeforeTheScholarship.Tests.Unit.Base.Services.Debts
{
    public class DebtsDataHelper
    {
        private readonly DbContextHelper _dbContextHelper = new();

        public IEnumerable<DebtResponse> GenerateDebtResponses(Guid? studentId = null)
        {
            using var context = _dbContextHelper.GetContextData();

            // If studentId is null returns all debts, if its not then returns debts which StudentId field is studentId
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

        public CreateDebtModel GenerateCreateDebtModel()
        {
            return new CreateDebtModel
            {
                StudentId = ExistedStudentsUuids.SecondGuid,
                Borrowed = new Random().Next(50, 500),
                Phone = Guid.NewGuid().ToString().Divide(),
                BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
            };
        }

        public UpdateDebtModel GenerateUpdateDebtModel()
        {
            return new UpdateDebtModel
            {
                Borrowed = new Random().Next(50, 500),
                Phone = Guid.NewGuid().ToString().Divide(),
                BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                WhenToPayback = DateTimeOffset.Now.AddDays(3),
            };
        }
    }
}
