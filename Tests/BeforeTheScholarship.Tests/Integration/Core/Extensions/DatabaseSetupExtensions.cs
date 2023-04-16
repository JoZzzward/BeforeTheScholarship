using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Integration.Base.Data;
using BeforeTheScholarship.Tests.Integration.Base.Data.Setup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
                opt.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            services.InitializeDatabase();
        }

        private static void CreateStudentUser(this IServiceCollection services)
        {
            var scopeProvider = services.BuildServiceProvider();
            var userManager = scopeProvider.GetRequiredService<UserManager<StudentUser>>();

            userManager.CreateAsync(new StudentUser
            {
                Id = StudentConsts.Id,
                UserName = StudentConsts.UserName,
                Email = StudentConsts.Email
            }, StudentConsts.Password);
        }

        private static void InitializeDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var db = serviceProvider.GetRequiredService<AppDbContext>();

            var debts = DataSeeder.InitializingDebts();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            services.CreateStudentUser();

            db.Debts.AddRange(debts);
            db.SaveChanges();
        }
    }
}
