using BeforeTheScholarship.DebtService;
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

            .AddStudentService()
            .AddDebtService()

            .AddEmailSender()
            .AddUserAccountService();

        return services;
    }
}
