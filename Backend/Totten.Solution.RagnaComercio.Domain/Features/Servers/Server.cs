namespace Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.Domain.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public record Server : Entity<Server, Guid>, IActive
{
    public string DisplayName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string SiteUrl { get; set; } = string.Empty;

    public virtual List<Agent> Agents { get; set; } = [];
    public virtual List<Callback> Callbacks { get; set; } = [];
}