namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.CommandsHandler;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Agents.Commands;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;
using Unit = LanguageExt.Unit;

public class AgentCreateCommandHandler : IRequestHandler<AgentCreateCommand, Result<Unit>>
{
    private IMapper _mapper;
    private IAgentRepository _repository;
    public AgentCreateCommandHandler(IAgentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<Unit>> Handle(AgentCreateCommand request, CancellationToken cancellationToken)
    {
        var agent = _mapper.Map<Agent>(request);

        return await _repository.Save(agent);
    }
}
