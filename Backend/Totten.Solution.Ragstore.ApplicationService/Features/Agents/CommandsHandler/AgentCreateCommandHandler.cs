namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.CommandsHandler;

using AutoMapper;
using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Agents.Commands;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;


public class AgentCreateCommandHandler : IRequestHandler<AgentCreateCommand, Result<Success>>
{
    private IMapper _mapper;
    private IAgentRepository _repository;
    public AgentCreateCommandHandler(IAgentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Success>> Handle(AgentCreateCommand request, CancellationToken cancellationToken)
    {
        var agent = _mapper.Map<Agent>(request);

        return await _repository.Save(agent);
    }
}
