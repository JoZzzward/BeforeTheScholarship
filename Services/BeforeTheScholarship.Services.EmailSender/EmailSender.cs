namespace BeforeTheScholarship.Services.EmailSender;

using BeforeTheScholarship.Services.EmailSender.Management;
using BeforeTheScholarship.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class EmailSender : Manager, IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(
        ILogger<EmailSender> logger,
        EmailSettings settings,
        IConfiguration configuration
        ) : base(logger, settings, configuration)
    {
        _logger = logger;
    }

    public async Task SendEmail(EmailModel model)
    {
        _logger.LogInformation("--> Trying to send email to {EmailTo}", model.EmailTo);

        var email = InitializeMessage(model);

        SendMessage(email);
    }
}
