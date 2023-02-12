using BeforeTheScholarship.Context.Settings;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Context.Factories;

public class DbContextOptionsFactory
{
    public async void Create(string connStr, DbType Type)
    {
        var bldr = new DbContextOptionsBuilder<AppDbContext>();

        Configure(connStr, Type).Invoke(bldr);
    }

	public static Action<DbContextOptionsBuilder> Configure(string connStr, DbType Type)
    {
        return (bldr) =>
        {
            switch(Type)
            {
                case DbType.PostgreSQL:     
                    bldr.UseNpgsql(o =>
                    {
                        o.CommandTimeout(TimeSpan.FromMinutes(10).Seconds);
                        o.MigrationsHistoryTable("EFMigrationHistory", "public");
                        o.MigrationsAssembly($"{DbConsts.migrationAssembly}.{Type}");
                    });
                break;
            }

            bldr.EnableSensitiveDataLogging();
            bldr.EnableDetailedErrors();
        };
    }
}
