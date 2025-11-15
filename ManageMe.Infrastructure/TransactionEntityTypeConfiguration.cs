using ManageMe.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageMe.Infrastructure;

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
            .IsRequired()
            .HasColumnType("DECIMAL (19,4)");

        builder.Property(t => t.Description)
            .IsRequired();

        builder.Property(t => t.Movement)
            .HasConversion(
                v => (int)v,
                v => v == 0 ? Movement.OUTGOING : Movement.INCOMING
            )
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .ValueGeneratedOnAdd();
    }
}
