namespace Totten.Solution.Ragstore.Domain.Features.AgentAggregation;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public record Agent : Entity<Agent, Guid>, IActive
{
    public bool IsActive { get; set; }
    public Guid ServerId { get; set; }
    public virtual Server? Server { get; set; }
}
