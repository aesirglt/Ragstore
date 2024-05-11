namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class AgentCreateCommand : IRequest<Result<Exception, Unit>>
{
    public string Name { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
}
