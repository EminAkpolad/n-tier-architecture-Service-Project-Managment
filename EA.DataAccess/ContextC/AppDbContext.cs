using EA.Core.Entities;
using EA.Core.Commons;
using Microsoft.EntityFrameworkCore;

namespace EA.DataAccess.ContextC;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    public DbSet<AppUser> Users{get; set;}
    public DbSet<UserClaim> UserClaims{get;set;}
    public DbSet<Commit> Commits{get; set;}
    public DbSet<Repository> Repositories{get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserClaim>()
        .HasOne<AppUser>()
        .WithMany(u=>u.UserClaims)
        .HasForeignKey(c=>c.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AppUser>()
        .HasIndex(u=>u.Email)
        .IsUnique();

        modelBuilder.Entity<UserClaim>()
        .HasIndex(c=>c.UserId);//İndex ekledim sorgu performansı arttı:)

        modelBuilder.Entity<Repository>()
        .Property(r=>r.Name)
        .IsRequired()
        .HasMaxLength(100);

        modelBuilder.Entity<Repository>()
        .HasOne(r=>r.AppUser)
        .WithMany(u=>u.Repositories)
        .HasForeignKey(r=>r.AppUserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}