namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Accounts.Queries;

using FunctionalConcepts.Results;
using MediatR;
using System.Linq;
using Totten.Solution.RagnaComercio.Domain.Features.Accounts;

public class AccountByIdQuery : IRequest<Result<IQueryable<Account>>>
{
}
