namespace Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;
using Totten.Solution.RagnaComercio.Domain.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public record Agent : Entity<Agent, Guid>, IActive
{
    public bool IsActive { get; set; }
    public Guid ServerId { get; set; }
    public virtual Server? Server { get; set; }
}
