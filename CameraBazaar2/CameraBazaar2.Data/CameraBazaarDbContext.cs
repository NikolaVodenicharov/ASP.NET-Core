using CameraBazaar2.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CameraBazaar2.Data
{
    public class CameraBazaarDbContext : IdentityDbContext<User>
    {
        public CameraBazaarDbContext(DbContextOptions<CameraBazaarDbContext> options)
            : base(options)
        {
        }

        public DbSet<Camera> Cameras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Camera>()
                .HasKey(c => c.Id);

            builder.Entity<Camera>()
                .HasOne(c => c.User)
                .WithMany(u => u.Cameras)
                .HasForeignKey(c => c.UserId);
        }
    }
}
