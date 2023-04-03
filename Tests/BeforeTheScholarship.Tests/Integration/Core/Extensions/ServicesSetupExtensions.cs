using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.UserAccountService;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Core.Setup
{
    public static class ServicesSetupExtensions
    {
        public static void ServicesSetup(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDebtService, DebtService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
        }
    }
}
