using Domain.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class TaskConfiguration : IEntityTypeConfiguration<TaskDetail>
{
    public void Configure(EntityTypeBuilder<TaskDetail> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.Property(c => c.Description).HasMaxLength(500);

        builder.Property(p => p.Id).HasConversion(
            productId => productId.Value,
            value => new TaskId(value));
    }
}
