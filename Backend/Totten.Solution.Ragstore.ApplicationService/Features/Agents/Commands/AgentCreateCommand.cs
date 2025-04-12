namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class AgentCreateCommand : IRequest<Result<Success>>
{
    public int ServerId { get; set; }
    public string Name { get; set; } = string.Empty;
}
