namespace Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.Domain.Features.Users;
using Totten.Solution.RagnaComercio.Infra.Data.Features.Agents.EntityConfigurations;
using Totten.Solution.RagnaComercio.Infra.Data.Features.CallbackAggregation.EntityConfigurations;
using Totten.Solution.RagnaComercio.Infra.Data.Features.Servers.EntityConfigurations;

public class RagnaStoreContext : DbContext
{
    public virtual DbSet<Server> Servers { get; set; }
    public virtual DbSet<Callback> Callbacks { get; set; }
    public virtual DbSet<CallbackSchedule> CallbacksSchedule { get; set; }
    public virtual DbSet<Agent> UpdateTimes { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public RagnaStoreContext(DbContextOptions<RagnaStoreContext> options) : base(options)
    {
        //if (Database.IsRelational())
        //{
        //    Database?.Migrate();
        //}
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => base.OnModelCreating(
                modelBuilder.ApplyConfiguration(new ServerEntityConfiguration())
                            .ApplyConfiguration(new AgentEntityConfiguration())
                            .ApplyConfiguration(new CallbackScheduleConfiguration())
                            .ApplyConfiguration(new CallbackEntityConfiguration()));
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }
}
