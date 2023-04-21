using BeforeTheScholarship.Web.Pages.Auth;

namespace BeforeTheScholarship.Web
{
    public static class Bootstrapper
    {

        public static void RegisterClientServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();


        }

    }
}
