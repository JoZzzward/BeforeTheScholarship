using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Context;

/// <summary>
/// Creates a new app db context
/// </summary>
public class DbContextFactory
{
    private readonly DbContextOptions<AppDbContext> _options;

    public DbContextFactory(DbContextOptions<AppDbContext> options) => 
        _options = options;

    public AppDbContext Create() => 
        new AppDbContext(_options);
}
