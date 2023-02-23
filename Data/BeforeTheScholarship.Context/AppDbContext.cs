using BeforeTheScholarship.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Context;

public class AppDbContext : IdentityDbContext<StudentUser, IdentityRole<Guid>, Guid>
{
    public DbSet<StudentUser> StudentUsers { get; set; }
    public DbSet<Debts> Debts { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options)
		:base (options) 
    { 
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentUser>().ToTable("StudentUsers");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("StudentUsersRoles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("StudentUsersTokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("StudentUsersRoleOwners");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("StudentUserRoleClaims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("StudentUsersLogins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("StudentUsersClaims");

        // Initializing StudentUser model
        modelBuilder.Entity<StudentUser>().Property(x => x.UserName).IsRequired();
        modelBuilder.Entity<StudentUser>().Property(x => x.PhoneNumber).HasMaxLength(11);
        modelBuilder.Entity<StudentUser>().Property(x => x.Email).HasMaxLength(50);
        modelBuilder.Entity<StudentUser>()
            .HasMany(x => x.Debts)
            .WithOne(x => x.StudentUser)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);

        // Initializing Debts model
        modelBuilder.Entity<Debts>().ToTable("Debts");
        modelBuilder.Entity<Debts>().Property(x => x.BorrowedFromWho).IsRequired();
        modelBuilder.Entity<Debts>().Property(x => x.Borrowed).IsRequired();

    }
}
