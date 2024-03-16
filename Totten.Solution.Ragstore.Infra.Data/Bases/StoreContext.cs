namespace Totten.Solution.Ragstore.Infra.Data.Bases;

using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Domain.Features.Stores;

public class StoreContext : DbContext
{
    public virtual DbSet<Store> Stories { get; set; }

    public StoreContext(DbContextOptions<StoreContext> configuration) : base(configuration)
    {
        //Database?.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new StoreEntityConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
