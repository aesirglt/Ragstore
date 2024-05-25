namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.Commands;

using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class AgentCreateCommand : IRequest<Result<Unit>>
{
    public string Name { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
}
