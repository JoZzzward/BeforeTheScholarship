using BeforeTheScholarship.Services.EmailSender;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Helpers.EmailSenderService
{
    public static partial class Bootstrapper
    {
        public static void AddFakeEmailSenderService(this IServiceCollection services)
        {
            var emailSenderService = EmailSenderHelper.Initialize();

            services.AddSingleton(typeof(IEmailSender), emailSenderService);
        }
    }
}
