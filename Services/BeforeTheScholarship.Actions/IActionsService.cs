using BeforeTheScholarship.Services.EmailSender;

namespace BeforeTheScholarship.Actions;

public interface IActionsService
{
    Task SendDebtEmail(DebtEmailModel model, double delay);
}