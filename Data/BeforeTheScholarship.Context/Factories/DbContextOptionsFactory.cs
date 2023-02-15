using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Context;

public class DbContextOptionsFactory
{
	public static Action<DbContextOptionsBuilder> Configure(string connStr, DbType Type)
    {
        return (bldr) =>
        {
            switch(Type)
            {
                case DbType.PostgreSQL:     
                    bldr.UseNpgsql(
                        connStr, 
                        o =>
                            {
                                o.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                                o.MigrationsHistoryTable("EFMigrationHistory", "public");
                                o.MigrationsAssembly($"{DbConsts.MigrationAssembly}{Type}");
                            });
                break;
            }

            bldr.EnableSensitiveDataLogging();
            bldr.EnableDetailedErrors();
            bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };
    }
}
