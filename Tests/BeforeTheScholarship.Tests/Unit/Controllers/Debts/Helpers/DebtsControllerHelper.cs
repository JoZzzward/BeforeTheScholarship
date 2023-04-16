using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.DebtService.Models;
using BeforeTheScholarship.Tests.Unit.Controllers.Debts.Consts;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers;

namespace BeforeTheScholarship.Tests.Unit.Controllers.Debts.Helpers
{
    public static class DebtsControllerHelper
    {
        private static IDebtService _debtService;

        public static IDebtService Initialize()
        {
            _debtService = Substitute.For<IDebtService>();

            return _debtService;
        }

        public static class Setup
        {
            private static readonly DebtsDataHelper _debtsDataHelper = new();

            public static void SetupGetDebtsReturnsData()
            {
                var content = _debtsDataHelper.GenerateDebtResponses();

                _debtService.GetDebts().Returns(content);
            }

            public static void SetupGetDebtsWithStudentIdReturnsData()
            {
                Guid? studentId = ExistedStudentConsts.Id;
                var content = _debtsDataHelper.GenerateDebtResponses(studentId);

                _debtService.GetDebts(studentId).Returns(content);
            }

            public static void SetupCreateDebtReturnsData()
            {
                var response = new CreateDebtResponse
                {
                    StudentId = ExistedStudentConsts.Id
                };

                _debtService.CreateDebt(Arg.Any<CreateDebtModel>()).Returns(response);
            }

            public static void SetupUpdateDebtReturnsData()
            {
                var response = new UpdateDebtResponse
                {
                    Uid = ExistedDebtConsts.Uid
                };

                _debtService.UpdateDebt(Arg.Any<Guid>(), Arg.Any<UpdateDebtModel>()).Returns(response);
            }

            public static void SetupDeleteDebtReturnsData()
            {
                var response = new DeleteDebtResponse
                {
                    Uid = ExistedDebtConsts.Uid
                };

                _debtService.DeleteDebt(Arg.Any<Guid>()).Returns(response);
            }

            public static void SetupGetUrgentlyRepaidDebtsReturnsData()
            {
                var studentId = ExistedStudentConsts.Id;

                var content = _debtsDataHelper.GenerateDebtResponses(studentId);

                content = content.ToList()
                    .Where(x => x.WhenToPayback > DateTimeOffset.UtcNow &&
                                x.WhenToPayback <= DateTimeOffset.UtcNow.AddDays(1));

                _debtService.GetUrgentlyRepaidDebts(studentId).Returns(content);
            }

            public static void SetupGetOverdueDebtsReturnsEmptyData()
            {
                Guid studentId = ExistedStudentConsts.Id;

                var content = _debtsDataHelper.GenerateDebtResponses(studentId);

                content = content.ToList().Where(x => (x.WhenToPayback - DateTimeOffset.Now).TotalDays <= 0);

                _debtService.GetOverdueDebts(studentId).Returns(content);
            }
        }
    }
}
