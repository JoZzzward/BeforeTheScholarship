namespace BeforeTheScholarship.Services.EmailSender;

using BeforeTheScholarship.Common.Security;
using BeforeTheScholarship.Services.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly EmailSettings _settings;
    private readonly IConfiguration _configuration;

    public EmailSender(
        ILogger<EmailSender> logger,
        EmailSettings settings,
        IConfiguration configuration
        )
    {
        _logger = logger;
        _settings = settings;
        _configuration = configuration;
    }

    public async Task SendDebtEmail(DebtEmailModel model)
    {
        _logger.LogInformation("Trying to send debt email to {EmailTo}", model.EmailTo);

        var email = InitializeMessage<DebtEmailModel>(model);

        SendMessage(email);

        _logger.LogInformation("Debt email sended to: {EmailTo}; ", model.EmailTo);
    }

    public async Task SendEmail(EmailModel model)
    {
        _logger.LogInformation("Trying to send email to {EmailTo}", model.EmailTo);

        var email = InitializeMessage<EmailModel>(model);

        SendMessage(email);

        _logger.LogInformation("Email sended to: {EmailTo}; ", model.EmailTo);
    }

    private MimeMessage InitializeMessage<T>(dynamic model)
    {
        InitializeCredentials(out string emailFrom, out string password);

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(emailFrom));
        email.To.Add(MailboxAddress.Parse(model.EmailTo));
        email.Subject = model.Subject;

        email.Body = new TextPart(TextFormat.Html)
        {
            Text = model.Message
        };

        return email;
    }

    private void SendMessage(MimeMessage email)
    {
        InitializeCredentials(out string emailFrom, out string password);

        using var smtp = new SmtpClient();
        smtp.Connect(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(emailFrom, password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    private void InitializeCredentials(out string emailFrom, out string password)
    {
        emailFrom = SecretSearcher.SearchSecret("EmailSecretValue:Username", "emailusername", _configuration);
        password = SecretSearcher.SearchSecret("EmailSecretValue:Password", "emailpassword", _configuration);
    }
}
