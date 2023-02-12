namespace BeforeTheScholarship.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{

    public AppDbContext CreateDbContext(string[] args)
    {
        var provider = (args?[0] ?? $"{DbType.PostgreSQL}").ToLower();

        var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.context.json")
             .Build();


        DbContextOptions<AppDbContext> options;
        if (provider.Equals($"{DbType.PostgreSQL}".ToLower()))
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql(
                        configuration.GetConnectionString(provider),
                        opts => opts
                            .MigrationsAssembly($"{DbConsts.MigrationAssembly}{DbType.PostgreSQL}")
                    )
                    .Options;
        }
        else
        {
            throw new Exception($"Unsupported provider: {provider}");
        }

        var dbf = new DbContextFactory(options);
        return dbf.Create();
    }
}
