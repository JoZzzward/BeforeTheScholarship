using BeforeTheScholarship.Services.EmailSender;

namespace BeforeTheScholarship.Actions;

public interface IActionsService
{
    Task SendEmail(EmailModel model, double delay);
}