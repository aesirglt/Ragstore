namespace Totten.Solution.Ragstore.ApplicationService.Features.Accounts.Queries;

using MediatR;
using System;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class AccountByIdQuery : IRequest<Result<Exception, IQueryable<Account>>>
{

}
