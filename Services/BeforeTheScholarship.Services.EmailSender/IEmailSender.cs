namespace BeforeTheScholarship.Services.EmailSender;

public interface IEmailSender
{
    Task SendEmail(EmailModel model);
    Task SendDebtEmail(DebtEmailModel model);
}