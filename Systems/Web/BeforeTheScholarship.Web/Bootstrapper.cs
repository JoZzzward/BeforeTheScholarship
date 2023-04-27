using BeforeTheScholarship.Web.Pages.Auth;
using BeforeTheScholarship.Web.Pages.Debts.Services;
using BeforeTheScholarship.Web.Pages.Profile.Services;

namespace BeforeTheScholarship.Web
{
    public static class Bootstrapper
    {
        public static void RegisterClientServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDebtService, DebtService>(); 
            services.AddScoped<IStudentService, StudentService>(); 
        }
    }
}
