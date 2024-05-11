namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class UserCreateCommand : IRequest<Result<Exception, Unit>>
{
}
