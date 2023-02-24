using BeforeTheScholarship.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Context;

/// <summary>
/// Database seeder
/// </summary>
public static class DbSeeder
{
    public static IServiceScope ScopeService(IServiceProvider provider)
        => provider
            .GetService<IServiceScopeFactory>()
            !.CreateScope();
    public static AppDbContext AppDbContext(IServiceProvider provider)
        => ScopeService(provider)
            .ServiceProvider
            .GetRequiredService<IDbContextFactory<AppDbContext>>()
            .CreateDbContext();
    
    /// <summary>
    /// Execute DatabaseSeeder and adding the data
    /// </summary>
    public static void Execute(IServiceProvider provider, bool addData)
    {
        using var scope = ScopeService(provider);
        ArgumentNullException.ThrowIfNull(scope);

        if (addData)
        {
            Task.Run(async () =>
            {
                await ConfigureData(provider);
            });
        }
    }

    private static async Task ConfigureData(IServiceProvider provider)
    {
        await AddData(provider);
    }
    /// <summary>
    /// Data that will be added in database by launching the app
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static async Task AddData(IServiceProvider provider)
    {
        await using var context = AppDbContext(provider);

        #region Adding models
        var usersArray = context.StudentUsers.Select(x => x.Id).ToArray();

        if (!context.Debts.Any() && 
            context.StudentUsers.Count() > 2)
        {
            var debtsList = new List<Debts>()
            {
                new Debts()
                {
                    Id = 1,
                    StudentId = usersArray[0],
                    Borrowed = 100,
                    BorrowedFromWho = "John"
                },
                new Debts()
                {
                    Id = 2,     
                    StudentId = usersArray[0],
                    Borrowed = 200,
                    BorrowedFromWho = "Jastin"
                },
                new Debts()
                {
                    Id = 3,
                    StudentId = usersArray[1],
                    Borrowed = 300,
                    BorrowedFromWho = "Nolan"
                }
            };

            context.Debts.AddRange(debtsList);
        }
        #endregion
        context.SaveChanges();
    }
}
