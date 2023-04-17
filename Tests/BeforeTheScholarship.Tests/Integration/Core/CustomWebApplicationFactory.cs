using BeforeTheScholarship.Common.Security;
using BeforeTheScholarship.Tests.Integration.Core.Authorization;
using BeforeTheScholarship.Tests.Integration.Core.Setup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeforeTheScholarship.Tests.Integration.Core
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
                builder.ConfigureServices(services =>
                {
                    services.ServicesSetup();
                    services.ConfigurationSetup();
                    services.DatabaseSetup();
                });
                return base.CreateHost(builder);
        }

        /// <summary>
        /// Creates and returns <see cref="HttpClient"/> with default setup settings.
        /// <para></para>
        /// Default setup BaseAddress to API is: "http://localhost:7000/api/v1/"
        /// </summary>
        public HttpClient SetupClient()
        {
            var client = CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });

            client.BaseAddress = new Uri("http://localhost:7000/api/v1/");
            client.DefaultRequestVersion = new Version("1.0");

            return client;
        }
    }
}
