using BeforeTheScholarship.Context.Settings;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Context.Factories;

public class DbContextOptionsFactory
{
    public static DbContextOptions<AppDbContext> Create(string connStr, DbType Type)
    {
        var bldr = new DbContextOptionsBuilder<AppDbContext>();

        Configure(connStr, Type).Invoke(bldr);

        return bldr.Options;
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
                        o.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                        o.MigrationsHistoryTable("EFMigrationHistory", "public");
                        o.MigrationsAssembly($"{DbConsts.migrationAssembly}{Type}");
                    });
                break;
            }

            bldr.EnableSensitiveDataLogging();
            bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };
    }
}
