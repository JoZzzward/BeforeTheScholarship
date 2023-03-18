using BeforeTheScholarship.Common.Security;
using BeforeTheScholarship.Services.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace BeforeTheScholarship.Services.EmailSender.Management;

public class Manager
{
    private readonly ILogger<EmailSender> _logger;
    private readonly EmailSettings _settings;
    private readonly IConfiguration _configuration;

    public Manager(
        ILogger<EmailSender> logger,
        EmailSettings settings,
        IConfiguration configuration
        )
    {
        _logger = logger;
        _settings = settings;
        _configuration = configuration;
    }

    protected MimeMessage InitializeMessage(EmailModel model)
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

        _logger.LogInformation("--> Message for specified email(Email: {EmailTo}) is initialized.}", model.EmailTo);

        return email;
    }

    protected void SendMessage(MimeMessage email)
    {
        InitializeCredentials(out string emailFrom, out string password);

        using var smtp = new SmtpClient();
        smtp.Connect(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(emailFrom, password);
        smtp.Send(email);
        smtp.Disconnect(true);

        _logger.LogInformation("--> Email sended to: {EmailTo}; ", email.To);
    }

    protected void InitializeCredentials(out string emailFrom, out string password)
    {
        emailFrom = SecretSearcher.SearchSecret("EmailSecretValue:Username", "emailusername", _configuration);
        password = SecretSearcher.SearchSecret("EmailSecretValue:Password", "emailpassword", _configuration);
    }
}
