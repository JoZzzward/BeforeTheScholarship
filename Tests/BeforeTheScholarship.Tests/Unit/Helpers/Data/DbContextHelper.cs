using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Tests.Unit.Helpers.Data.Setup;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Tests.Unit.Helpers.Data
{
    public class DbContextHelper : IDisposable
    {
        private AppDbContext _dbContext = CreateAppDbContext();

        public AppDbContext GetContextData(bool seedData = true)
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            if (seedData)
                DataSeeder.SeedData(ref _dbContext);

            return _dbContext;
        }
        
        public class AppDbContextFactory : IDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext()
            {
                return CreateAppDbContext();
            }
        }

        public AppDbContext CreateContext()
        {
            return CreateAppDbContext();
        }

        public IDbContextFactory<AppDbContext> CreateDbContextFactory()
        {
            return new AppDbContextFactory();
        }

        private static AppDbContext CreateAppDbContext()
        {
            return new AppDbContext(
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().Shrink())
                    .Options);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
