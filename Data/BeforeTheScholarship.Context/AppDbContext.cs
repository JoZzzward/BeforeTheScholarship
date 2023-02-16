using BeforeTheScholarship.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.Context;

public class AppDbContext : DbContext
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

        //TODO: Initialize StudentUser as IdentityUser model

        // Initializing StudentUser model
        modelBuilder.Entity<StudentUser>().Property(x => x.UserName).IsRequired();
        modelBuilder.Entity<StudentUser>().Property(x => x.Phone).HasMaxLength(12);
        modelBuilder.Entity<StudentUser>().Property(x => x.Email).HasMaxLength(50);
        modelBuilder.Entity<StudentUser>()
            .HasMany(x => x.Debts)
            .WithOne(x => x.StudentUser)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);

        // Initializing Debts model
        modelBuilder.Entity<Debts>().Property(x => x.BorrowedFromWho).IsRequired();
        modelBuilder.Entity<Debts>().Property(x => x.Borrowed).IsRequired();

    }
}
