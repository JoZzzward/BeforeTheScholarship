namespace BeforeTheScholarship.Services.EmailSender;

using BeforeTheScholarship.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<EmailSettings>("EmailSettings", configuration);
        services.AddSingleton(settings);

        services.AddSingleton<IEmailSender, EmailSender>();

        return services;
    }
}
