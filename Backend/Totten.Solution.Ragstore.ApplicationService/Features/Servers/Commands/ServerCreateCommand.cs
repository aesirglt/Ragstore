namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class ServerCreateCommand : IRequest<Result<Exception, Unit>>
{
    public string Name { get; set; } = string.Empty;
    public string SiteUrl { get; set; } = string.Empty;
}
