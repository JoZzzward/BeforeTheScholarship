using BeforeTheScholarship.Api.Configuration;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Integration.Core.Setup;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BeforeTheScholarship.Tests.Integration.Core
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();
            builder.ConfigureServices(services =>
            {
                services.ConfigurationSetup();
                
                services.DatabaseSetup();

                services.ServicesSetup();
            });
            return base.CreateHost(builder);
        }
    }
}
