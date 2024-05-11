namespace Totten.Solution.Ragstore.Infra.Data.Features.Agents;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public class UpdateTimeRepository(ServerStoreContext context)
    : RepositoryBase<Agent>(context), IAgentRepository
{
}
