using BeforeTheScholarship.Web.Pages.Debts.Consts.Statuses;

namespace BeforeTheScholarship.Web.Pages.Debts.Enums
{
    public static class DebtStatus
    {
        public static readonly OverdueDebt OverdueDebt = new();
        public static readonly UrgentlyRepaidDebt UrgentlyRepaidDebt = new();
        public static readonly SimpleDebt SimpleDebt = new();
    }
}
