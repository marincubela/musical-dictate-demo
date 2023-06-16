using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Identity;

namespace Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder
            .HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Student>(s => s.Id)
            .IsRequired();

        builder
            .HasMany(s => s.MyGroups)
            .WithMany(group => group.Students);
    }
}