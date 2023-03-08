using BeforeTheScholarship.Actions;
using BeforeTheScholarship.DebtService;
using BeforeTheScholarship.RabbitMq;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.Settings;
using BeforeTheScholarship.Services.UserAccount;
using BeforeTheScholarship.StudentService;

namespace BeforeTheScholarship.Api;

/// <summary>
/// Loads all services
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Registers all services to application
    /// </summary>
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services.AddIdentitySettings()
            .AddSwaggerSettings()
            .AddRabbitMqService()
            .AddActionsService()
            .AddEmailSender()

            .AddStudentService()
            .AddDebtService()

            .AddUserAccountService();

        return services;
    }
}
