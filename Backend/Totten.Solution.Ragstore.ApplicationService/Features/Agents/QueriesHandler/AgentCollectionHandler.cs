namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.QueriesHandler;
using FunctionalConcepts.Results;

using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Agents.Queries;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class AgentCollectionHandler(IAgentRepository repository) : IRequestHandler<AgentCollectionQuery, Result<IQueryable<Agent>>>
{
    private readonly IAgentRepository _repository = repository;

    public async Task<Result<IQueryable<Agent>>> Handle(AgentCollectionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var characters = _repository.GetAll(x => x.ServerId == request.ServerId);
            return await Result.Of(characters).AsTask();
        }
        catch (Exception)
        {
            return await Result.Of(Array.Empty<Agent>().AsQueryable()).AsTask();
        }
    }
}
