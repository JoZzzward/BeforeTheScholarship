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
        
        private static AppDbContext CreateAppDbContext()
        {
            var databaseName = Guid.NewGuid().Shrink();
            return new AppDbContext(
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(databaseName)
                    .Options);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
