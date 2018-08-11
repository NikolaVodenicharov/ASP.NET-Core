namespace AspNetCoreIntro.Data
{
    using AspNetCoreIntro.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class CatsDbContext : DbContext
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options)
            :base(options)
        {

        }

        public DbSet<Cat> Cats { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
    }
}
