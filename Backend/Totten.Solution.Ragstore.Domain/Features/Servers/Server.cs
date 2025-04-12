namespace Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;

public record Server : Entity<Server, int>, IActive
{
    public bool IsActive { get; set; }
    public string SiteUrl { get; set; } = string.Empty;

    public virtual List<Agent> Agents { get; set; } = [];
    public virtual List<Callback> Callbacks { get; set; } = [];
}
