using EA.Core.Entities;
using EA.Core.Commons;
using Microsoft.EntityFrameworkCore;

namespace EA.DataAccess.ContextC;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    public DbSet<AppUser> Users{get; set;}
    public DbSet<Commit> Commits{get; set;}
    public DbSet<RepoSitory> Repositories{get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>()
        .HasIndex(u=>u.Email)
        .IsUnique();

        modelBuilder.Entity<RepoSitory>()
        .Property(r=>r.Name)
        .IsRequired()
        .HasMaxLength(100);

        modelBuilder.Entity<RepoSitory>()
        .HasOne(r=>r.AppUser)
        .WithMany(u=>u.Repositories)
        .HasForeignKey(r=>r.AppUserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}