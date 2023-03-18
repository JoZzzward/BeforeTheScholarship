namespace BeforeTheScholarship.Services.EmailSender;

public class DebtEmailModel : EmailModel
{
    public DateTimeOffset WhenToPayback { get; set; }
}
