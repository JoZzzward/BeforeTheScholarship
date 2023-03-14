namespace BeforeTheScholarship.Services.EmailSender;

public class DebtEmailModel
{
    public string EmailTo { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTimeOffset WhenToPayback { get; set; }
}
