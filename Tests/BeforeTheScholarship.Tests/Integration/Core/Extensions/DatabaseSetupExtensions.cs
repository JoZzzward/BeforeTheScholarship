using BeforeTheScholarship.Context;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using Microsoft.AspNetCore.Identity;

namespace BeforeTheScholarship.Tests.Integration.Core.Setup
{
    public static class DatabaseSetupExtensions
    {
        public static void DatabaseSetup(this IServiceCollection services)
        {
            services.RemoveAll(typeof(IDbContextFactory<AppDbContext>));
            services.RemoveAll(typeof(DbContextFactory<AppDbContext>));

            services.AddDbContextFactory<AppDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("InMemoryDb");
            });

            services.InitializeDatabase();

            services.CreateStudentUser();
        }

        private static void CreateStudentUser(this IServiceCollection services)
        {
            var scopeProvider = services.BuildServiceProvider();
            var userManager = scopeProvider.GetRequiredService<UserManager<StudentUser>>();
            userManager.CreateAsync(new StudentUser
            {
                UserName = StudentConsts.UserName,
                Email = StudentConsts.Email
            }, StudentConsts.Password);
        }

        private static void InitializeDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var db = serviceProvider.GetRequiredService<AppDbContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
