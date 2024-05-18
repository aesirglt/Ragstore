namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class ServerDeactiveCommand : IRequest<Result<Exception, Unit>>
{
    public int ServerId { get; set; }
}
