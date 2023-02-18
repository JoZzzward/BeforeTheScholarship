namespace BeforeTheScholarship.Services.EmailSender;

using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MimeKit;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly EmailSettings _settings;

    public EmailSender(
        ILogger<EmailSender> logger,
        EmailSettings settings
        )
    {
        _logger = logger;
        _settings = settings;
    }

    public async Task SendEmail(EmailModel model)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(model.EmailFrom));
            email.To.Add(MailboxAddress.Parse(model.EmailTo));
            email.Subject = model.Subject;

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = model.Message
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_settings.AuthenticateUsername, _settings.AuthenticatePassword);
            smtp.Send(email);
            smtp.Disconnect(true);

            _logger.LogInformation($"Email sended from: {model.EmailFrom}; \n" +
                                   $"Email sended to: {model.EmailTo}; \n");
        }
        catch (Exception ex)
        {
            _logger.LogError("ERROR at SendEmail --> " + ex.Message);
        }
    }
}
