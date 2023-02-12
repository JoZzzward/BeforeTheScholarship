using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Context.Factories;

public class DbContextFactory
{
    private readonly DbContextOptions<AppDbContext> _options;

    public DbContextFactory(DbContextOptions<AppDbContext> options)
	{
        _options = options;
    }

    public AppDbContext CreateDbContext()
    {
        return new AppDbContext(_options);
    }
}
