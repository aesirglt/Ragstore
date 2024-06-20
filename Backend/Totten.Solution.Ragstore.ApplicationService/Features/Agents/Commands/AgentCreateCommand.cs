namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.Commands;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;


public class AgentCreateCommand : IRequest<Result<Success>>
{
    public string Name { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
}
