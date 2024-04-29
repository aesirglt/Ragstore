namespace Totten.Solution.Ragstore.Infra.Data.Contexts.RagnaStoreContexts;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Domain.Features.Agents;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Data.Features.Agents.EntityConfigurations;
using Totten.Solution.Ragstore.Infra.Data.Features.Callbacks.EntityConfigurations;
using Totten.Solution.Ragstore.Infra.Data.Features.Servers.EntityConfigurations;

public class RagnaStoreContext : DbContext
{
    public virtual DbSet<Server> Servers { get; set; }
    public virtual DbSet<Callback> Callbacks { get; set; }
    public virtual DbSet<UpdateTime> UpdateTimes { get; set; }

    public RagnaStoreContext(DbContextOptions<RagnaStoreContext> options) : base(options)
    {
        Database?.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => base.OnModelCreating(
                modelBuilder.ApplyConfiguration(new ServerEntityConfiguration())
                            .ApplyConfiguration(new UpdateTimeEntityConfiguration())
                            .ApplyConfiguration(new CallbackEntityConfiguration()));

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => base.OnConfiguring(optionsBuilder.UseLazyLoadingProxies());

    public virtual IQueryable<T> AsNoTracking<T>(IQueryable<T> query) where T : class
        => EntityFrameworkQueryableExtensions.AsNoTracking<T>(query);
}
