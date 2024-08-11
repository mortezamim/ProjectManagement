using Domain.Projects;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasConversion(
            order => order.Value,
            value => new ProjectId(value));

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.Property(c => c.Description).HasMaxLength(500);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .IsRequired();

        builder.HasMany(o => o.Tasks)
            .WithOne()
            .HasForeignKey(li => li.ProjectId);
    }
}
