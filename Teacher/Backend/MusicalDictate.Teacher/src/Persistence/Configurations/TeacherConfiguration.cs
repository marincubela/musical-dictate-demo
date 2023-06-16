using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Identity;

namespace Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder
            .HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Teacher>(t => t.Id)
            .IsRequired();
    }
}