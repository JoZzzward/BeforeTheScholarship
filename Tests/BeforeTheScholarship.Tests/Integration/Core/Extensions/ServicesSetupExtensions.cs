using BeforeTheScholarship.Services.Actions;
using BeforeTheScholarship.Services.CacheService;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.RabbitMqService;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.UserAccountService;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Core.Setup
{
    public static class ServicesSetupExtensions
    {
        public static void ServicesSetup(this IServiceCollection services)
        {
            services.AddStudentService();
            services.AddDebtService();
            services.AddUserAccountService();

            services.AddCacheService();
            services.AddRabbitMqService();
            services.AddActionsService();
            services.AddEmailSender();
        }
    }
}
