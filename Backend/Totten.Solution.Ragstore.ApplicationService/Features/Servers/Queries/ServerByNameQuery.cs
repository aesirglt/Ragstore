namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;

using MediatR;
using System;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ServerByNameQuery : IRequest<Result<Exception, Server>>
{
    public string Name { get; set; } = string.Empty;
}
