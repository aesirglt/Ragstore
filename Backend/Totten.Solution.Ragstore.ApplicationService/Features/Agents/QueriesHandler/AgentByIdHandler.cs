namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.QueriesHandler;

using AutoMapper;
using FunctionalConcepts.Results;

using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Agents.Queries;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;

public class AgentByIdHandler(IAgentRepository repository, IMapper mapper) : IRequestHandler<AgentByIdQuery, Result<IQueryable<Agent>>>
{
    private readonly IAgentRepository _agentRepository = repository;
    private readonly IMapper _mapper = mapper;

    public  Task<Result<IQueryable<Agent>>> Handle(AgentByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Task.FromResult(Result.Of(_agentRepository.GetAll()));
        }
        catch (Exception)
        {
            return Task.FromResult(Result.Of(Array.Empty<Agent>().AsQueryable()));
        }
    }
}
