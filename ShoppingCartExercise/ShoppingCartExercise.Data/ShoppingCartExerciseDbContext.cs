using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCartExercise.Data.Models;

namespace ShoppingCartExercise.Data
{
    public class ShoppingCartExerciseDbContext : IdentityDbContext<User>
    {
        public ShoppingCartExerciseDbContext(DbContextOptions<ShoppingCartExerciseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder
                .Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            base.OnModelCreating(builder);
        }
    }
}
