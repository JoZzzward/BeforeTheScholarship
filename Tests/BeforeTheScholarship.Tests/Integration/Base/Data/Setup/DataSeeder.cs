using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Tests.Unit.Controllers.Debts.Consts;
using Microsoft.AspNetCore.Identity;

namespace BeforeTheScholarship.Tests.Integration.Base.Data.Setup
{
    public static class DataSeeder
    {
        public static IEnumerable<Debts> InitializingDebts()
        {
            var debts = new List<Debts>
            {
                new()
                {
                    Uid = ExistedDebtConsts.Uid,
                    StudentId = StudentConsts.Id,
                    Borrowed = new Random().Next(50, 500),
                    Phone = "55544433322",
                    BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                    WhenBorrowed = DateTimeOffset.UtcNow,
                    WhenToPayback = DateTimeOffset.UtcNow.AddDays(2)
                },
                new()
                {
                    Uid = ExistedDebtConsts.SecondUid,
                    StudentId = StudentConsts.Id,
                    Borrowed = new Random().Next(50, 500),
                    Phone = "44433332222",
                    BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                    WhenBorrowed = DateTimeOffset.UtcNow,
                    WhenToPayback = DateTimeOffset.UtcNow.AddDays(3)
                },
                new()
                {
                    Uid = ExistedDebtConsts.ThirdUid,
                    StudentId = StudentConsts.Id,
                    Borrowed = new Random().Next(50, 500),
                    Phone = "1234567891",
                    BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                    WhenBorrowed = DateTimeOffset.UtcNow,
                    WhenToPayback = DateTimeOffset.UtcNow.AddDays(1)
                },
                new() // Overdue on 5 days
                {
                    Uid = Guid.NewGuid(),
                    StudentId = StudentConsts.Id,
                    Borrowed = new Random().Next(50, 500),
                    Phone = "44433332222",
                    BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                    WhenBorrowed = DateTimeOffset.UtcNow,
                    WhenToPayback = DateTimeOffset.UtcNow.AddDays(-5)
                },
                new() // Must be urgently repaid
                {
                    Uid = Guid.NewGuid(),
                    StudentId = StudentConsts.Id,
                    Borrowed = new Random().Next(50, 500),
                    Phone = "1234567891",
                    BorrowedFromWho = Guid.NewGuid().Shrink().Divide(4),
                    WhenBorrowed = DateTimeOffset.UtcNow,
                    WhenToPayback = DateTimeOffset.UtcNow.AddHours(22)
                }
            };

            return debts;
        }

        public static IEnumerable<StudentUser> InitializingStudents()
        {
            var student = new StudentUser
            {
                Id = StudentConsts.Id,
                FirstName = "John",
                LastName = "Watson",
                UserName = StudentConsts.UserName,
                NormalizedUserName = StudentConsts.UserName.ToUpper(),
                Email = StudentConsts.Email,
                NormalizedEmail = StudentConsts.Email.ToUpper(),
                PhoneNumber = "12345678910",
                EmailConfirmed = false,
                AccessFailedCount = 0,
                SecurityStamp = "QS4ZRMIAEHFYVGD4XRADQ6PDZKLRJRBA",
                ConcurrencyStamp = "ca0b08f5-2804-49ae-81ac-99653a4e6678",
                LockoutEnabled = true,
                TwoFactorEnabled = false
            };

            student.PasswordHash = new PasswordHasher<StudentUser>().HashPassword(student, StudentConsts.Password);

            var students = new List<StudentUser>
            {
                student
            };

            return students;
        }
    }
}
