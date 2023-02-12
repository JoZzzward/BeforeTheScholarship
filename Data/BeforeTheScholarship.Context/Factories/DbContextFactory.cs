using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Context;

public class DbContextFactory
{
    private readonly DbContextOptions<AppDbContext> _options;

    public DbContextFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public AppDbContext Create()
    {
        return new AppDbContext(_options);
    }
}
