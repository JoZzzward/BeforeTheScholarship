using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Tests.Integration.Helpers.ActionsService;
using BeforeTheScholarship.Tests.Integration.Helpers.CacheService;
using BeforeTheScholarship.Tests.Integration.Helpers.EmailSenderService;
using BeforeTheScholarship.Tests.Integration.Helpers.RabbitMqService;
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

            services.AddFakeCacheService();
            services.AddFakeRabbitMqService();
            services.AddFakeActionsService();
            services.AddFakeEmailSenderService();
        }
    }
}
