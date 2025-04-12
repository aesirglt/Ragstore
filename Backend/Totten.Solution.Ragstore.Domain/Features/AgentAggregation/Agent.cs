namespace Totten.Solution.Ragstore.Domain.Features.AgentAggregation;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public record Agent : Entity<Agent, int>, IActive
{
    public bool IsActive { get; set; }
    public int ServerId { get; set; }
    public virtual Server? Server { get; set; }
}
