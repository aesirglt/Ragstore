namespace Totten.Solution.Ragstore.ApplicationService.Features.Accounts.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class AccountCreateCommand : IRequest<Result<Exception, Unit>>
{
}
