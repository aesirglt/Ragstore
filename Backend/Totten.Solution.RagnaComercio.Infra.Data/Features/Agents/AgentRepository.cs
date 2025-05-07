namespace Totten.Solution.RagnaComercio.Infra.Data.Features.Agents;
using Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;

public class AgentRepository(RagnaStoreContext context)
    : RepositoryBase<Agent, Guid>(context), IAgentRepository
{
}
