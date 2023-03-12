using BeforeTheScholarship.Actions;
using BeforeTheScholarship.Api.Configuration;
using BeforeTheScholarship.DebtService;
using BeforeTheScholarship.EmailWorker.EmailTask;
using BeforeTheScholarship.RabbitMq;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.StudentService;

namespace BeforeTheScholarship.EmailWorker;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddRabbitMqService()
            .AddActionsService()
            .AddEmailSender()
            .AddStudentService()
            .AddDebtService()
            .AddAppAutoMapper()
            ;

        services.AddSingleton<ITaskEmailSender, TaskEmailSender>();

        return services;
    }
}
