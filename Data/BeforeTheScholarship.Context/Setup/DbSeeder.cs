using BeforeTheScholarship.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Context;

public static class DbSeeder
{
    public static IServiceScope ScopeService(IServiceProvider provider)
        => provider.GetService<IServiceScopeFactory>()!.CreateScope();
    public static AppDbContext AppDbContext(IServiceProvider provider)
        => ScopeService(provider).ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext();

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

    public static async Task AddData(IServiceProvider provider)
    {
        await using var context = AppDbContext(provider);

        if (!context.StudentUsers.Any())
        {
            var studentUsers = new List<StudentUser>()
            { 
                new StudentUser()
                {
                    Id = 1,
                    Uid = Guid.NewGuid(),
                    UserName = "Clara",
                    Password = "password"
                },
                new StudentUser()
                {
                    Id = 2,
                    Uid = Guid.NewGuid(),
                    UserName = "John",
                    Password = "123"
                },
                new StudentUser()
                {
                    Id = 3,
                    Uid = Guid.NewGuid(),
                    UserName = "Lucy",
                    Password = "123123123"
                }
            };

            context.StudentUsers.AddRange(studentUsers);
        }
        if (!context.Debts.Any())
        {
            var debtsList = new List<Debts>()
            {
                new Debts()
                {
                    Id = 1,
                    StudentId = 1,
                    Uid = Guid.NewGuid(),
                    Borrowed = 100,
                    BorrowedFromWho = "John"
                },
                new Debts()
                {
                    Id = 2,     
                    StudentId = 2,
                    Uid = Guid.NewGuid(),
                    Borrowed = 200,
                    BorrowedFromWho = "Jastin"
                },
                new Debts()
                {
                    Id = 3,
                    StudentId = 1,
                    Uid = Guid.NewGuid(),
                    Borrowed = 300,
                    BorrowedFromWho = "Nolan"
                }
            };

            context.Debts.AddRange(debtsList);
        }

        context.SaveChanges();
    }
}
