using BeforeTheScholarship.Context.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BeforeTheScholarship.Context.Factories;

public class DbDesignTimeContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {   
        var provider = DbType.PostgreSQL.ToString().ToLower();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.context.json")
            .Build();

        DbContextOptions<AppDbContext> options;

        if (provider.Equals($"{DbType.PostgreSQL}".ToLower()))
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(
                    configuration.GetConnectionString(provider),    
                    opt =>
                        opt.MigrationsAssembly($"{DbConsts.migrationAssembly}{DbType.PostgreSQL.ToString()}")
                        
                ).Options;
        } else
        {
            throw new Exception($"Undefinded provider: {provider}");
        }

        var dcf = new DbContextFactory(options);

        return dcf.CreateDbContext();
    }
}
