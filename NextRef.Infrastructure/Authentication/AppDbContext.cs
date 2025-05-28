using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.Authentication;
public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .HasOne(a => a.UserEntity)
            .WithOne(u => u.AppUser)
            .HasForeignKey<UserEntity>(u => u.Id); // use same id
    }
}
