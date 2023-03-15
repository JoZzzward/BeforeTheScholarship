using BeforeTheScholarship.Api.Configuration;
using BeforeTheScholarship.EmailWorker.EmailTask;
using BeforeTheScholarship.Services.Actions;
using BeforeTheScholarship.Services.CacheService;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.RabbitMqService;
using BeforeTheScholarship.Services.StudentService;

namespace BeforeTheScholarship.EmailWorker;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddRabbitMqService()
            .AddActionsService()
            .AddEmailSender()
            .AddCacheService()

            .AddStudentService()
            .AddDebtService()

            .AddAppAutoMapper()
            ;

        services.AddSingleton<ITaskEmailSender, TaskEmailSender>();

        return services;
    }
}
