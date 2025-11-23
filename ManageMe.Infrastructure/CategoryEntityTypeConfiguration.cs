using ManageMe.Core;
using Microsoft.EntityFrameworkCore;

namespace ManageMe.Infrastructure;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.UserId)
            .IsRequired();

        builder
            .HasMany(c => c.Transactions)
            .WithOne()
            .HasForeignKey(t => t.Id);
    }
}
