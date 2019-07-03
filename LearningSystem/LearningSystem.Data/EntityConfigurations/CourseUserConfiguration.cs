using LearningSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Data.EntityConfigurations
{
    public class CourseUserConfiguration : IEntityTypeConfiguration<CourseUser>
    {
        public void Configure(EntityTypeBuilder<CourseUser> builder)
        {

            builder
                .HasKey(c => new { c.CourseId, c.UserId });

            builder
                .HasOne(cu => cu.User)
                .WithMany(u => u.CourseUsers)
                .HasForeignKey(cu => cu.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(cu => cu.Course)
                .WithMany(c => c.CourseUsers)
                .HasForeignKey(cu => cu.CourseId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
