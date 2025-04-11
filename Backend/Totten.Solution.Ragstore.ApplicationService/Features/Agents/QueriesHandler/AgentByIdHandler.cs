namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.QueriesHandler;
using FunctionalConcepts.Results;

using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Agents.Queries;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;

public class AgentByIdHandler : IRequestHandler<AgentByIdQuery, Result<IQueryable<Agent>>>
{
    public Task<Result<IQueryable<Agent>>> Handle(AgentByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
