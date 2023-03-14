using BeforeTheScholarship.Services.Actions;
using BeforeTheScholarship.Services.CacheService;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.RabbitMqService;
using BeforeTheScholarship.Services.Settings;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.UserAccount;

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
            .AddCacheService()

            .AddStudentService()
            .AddDebtService()

            .AddUserAccountService();

        return services;
    }
}
