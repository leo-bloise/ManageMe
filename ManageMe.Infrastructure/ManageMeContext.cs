using ManageMe.Core;
using Microsoft.EntityFrameworkCore;

namespace ManageMe.Infrastructure;

public class ManageMeContext: DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public ManageMeContext(DbContextOptions<ManageMeContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ManageMeContext).Assembly);
    }
}
