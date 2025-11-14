using ManageMe.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageMe.Infrastructure;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Email)
            .IsRequired();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder
            .Property(u => u.Name)
            .IsRequired();

        builder
            .Property(u => u.Password)
            .IsRequired();
    }
}
