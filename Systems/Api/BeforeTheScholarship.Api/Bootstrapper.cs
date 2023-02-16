using BeforeTheScholarship.DebtService;
using BeforeTheScholarship.StudentService;

namespace BeforeTheScholarship.Api;

/// <summary>
/// Registers all services to application
/// </summary>
public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services.AddStudentService()
            .AddDebtService();

        return services;
    }
}
