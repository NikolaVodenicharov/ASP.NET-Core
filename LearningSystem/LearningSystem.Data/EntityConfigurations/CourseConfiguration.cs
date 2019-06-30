using LearningSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Data.EntityConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasOne(c => c.Trainer)
                .WithMany(u => u.Trainings)
                .HasForeignKey(c => c.TrainerId);
        }
    }
}
