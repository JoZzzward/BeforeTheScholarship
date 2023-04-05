using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Unit.Controllers.Students.Consts;

namespace BeforeTheScholarship.Tests.Unit.Helpers.Data.Setup
{
    public static class DataSeeder
    {
        public static void SeedData(ref AppDbContext dbContext)
        {
            InitializingDebts(out IEnumerable<Debts> debtsList);
            InitializingStudents(out IEnumerable<StudentUser> studentsList);

            dbContext.Debts.AddRange(debtsList);
            dbContext.StudentUsers.AddRange(studentsList);
            dbContext.SaveChanges();
        }

        private static void InitializingDebts(out IEnumerable<Debts> debtsList)
        {
            debtsList = new List<Debts>()
            {
                new()
                {
                    StudentId = ExistedStudentsUuids.FirstGuid,
                    Borrowed = new Random().Next(50, 500),
                    Phone = Guid.NewGuid().ToString().Divide(),
                    BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                    WhenBorrowed = DateTimeOffset.UtcNow.LocalDateTime,
                    WhenToPayback = DateTimeOffset.UtcNow.LocalDateTime.AddDays(2)
                },
                new()
                {
                    StudentId = ExistedStudentsUuids.FirstGuid,
                    Borrowed = new Random().Next(50, 500),
                    Phone = Guid.NewGuid().ToString().Divide(),
                    BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                    WhenBorrowed = DateTimeOffset.UtcNow.LocalDateTime,
                    WhenToPayback = DateTimeOffset.UtcNow.LocalDateTime.AddDays(1)
                },
                new()
                {
                    StudentId = ExistedStudentsUuids.SecondGuid,
                    Borrowed = new Random().Next(50, 500),
                    Phone = Guid.NewGuid().ToString().Divide(),
                    BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                    WhenBorrowed = DateTimeOffset.MinValue,
                    WhenToPayback = DateTimeOffset.MinValue.AddDays(256)
                }
            };
        }

        private static void InitializingStudents(out IEnumerable<StudentUser> studentsList)
        {
            studentsList = new List<StudentUser>
            {
                new()
                {
                    Id = ExistedStudentsUuids.FirstGuid,
                    FirstName = "John",
                    LastName = "Watson",
                    UserName = "Jonny",
                    NormalizedUserName = "JONNY",
                    Email = "jozzzwardtm@mail.ru",
                    NormalizedEmail = "JOZZZWARDTM@MAIL.RU",
                    PhoneNumber = "12345678910",
                    EmailConfirmed = false,
                    AccessFailedCount = 0,
                    PasswordHash = "AQAAAAIAAYagAAAAEKd/9BgYLBn0SyDpRyTLYd5koe554mkbgy0trM0gGIUkiO7kjxdoFSQSrPUVCHJ2dw==",
                    SecurityStamp = "QS4ZRMIAEHFYVGD4XRADQ6PDZKLRJRBA",
                    ConcurrencyStamp = "ca0b08f5-2804-49ae-81ac-99653a4e6678",
                    LockoutEnabled = true,
                    TwoFactorEnabled = false
                },
                new()
                {
                    Id = ExistedStudentsUuids.SecondGuid,
                    FirstName = "Carla",
                    LastName = "Smith",
                    UserName = "Carlona",
                    NormalizedUserName = "CARLONA",
                    Email = "great.carla@mail.ru",
                    NormalizedEmail = "GREATCARLA@MAIL.RU",
                    PhoneNumber = "10987654321",
                    EmailConfirmed = false,
                    AccessFailedCount = 0,
                    PasswordHash = "AQAAAAIAAYagAAAAEOx7BBNoKTmYKWtpqO3pnsvb3uErWqxn+a+p1ZYJKNstHMZEf7AjnRjlvQgfBlOc+g==",
                    SecurityStamp = "UA45GR4CFYOHBXCYYML2TCUYFCJGM4OI",
                    ConcurrencyStamp = "9b9d63ef-fa11-4c46-a048-c9f14d5b8c04",
                    LockoutEnabled = true,
                    TwoFactorEnabled = false
                }
            };
        }
    }
}
