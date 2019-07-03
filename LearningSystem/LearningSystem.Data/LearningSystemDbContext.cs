using LearningSystem.Data.EntityConfigurations;
using LearningSystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningSystem.Data
{
    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }
 
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.ApplyConfiguration(new ArticleConfiguration());
            model.ApplyConfiguration(new CourseConfiguration());
            model.ApplyConfiguration(new CourseUserConfiguration());
        }
    }
}
