using BeforeTheScholarship.Services.EmailSender;

namespace BeforeTheScholarship.Services.Actions;

public interface IActionsService
{
    Task SendDebtEmail(DebtEmailModel model, double delay);
}