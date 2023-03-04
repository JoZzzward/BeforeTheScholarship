namespace BeforeTheScholarship.Services.EmailSender;

using BeforeTheScholarship.Services.Settings;
using BeforeTheScholarship.Common.Security;
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

    public async Task SendEmail(EmailModel model)
    {
        _logger.LogInformation("Trying to send email to {EmailTo}", model.EmailTo);

        var emailFrom = SecretSearcher.SearchSecret("EmailSecretValue:Username", "emailusername", _configuration);
        var password = SecretSearcher.SearchSecret("EmailSecretValue:Password", "emailpassword", _configuration);

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(emailFrom));
        email.To.Add(MailboxAddress.Parse(model.EmailTo));
        email.Subject = model.Subject;

        email.Body = new TextPart(TextFormat.Html)
        {
            Text = model.Message
        };

        using var smtp = new SmtpClient();
        smtp.Connect(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(emailFrom, password);
        smtp.Send(email);
        smtp.Disconnect(true);

        _logger.LogInformation("Email sended to: {EmailTo}; ", model.EmailTo);
    }
}
