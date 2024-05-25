namespace Totten.Solution.Ragstore.ApplicationService.Features.Accounts.Queries;

using LanguageExt.Common;
using MediatR;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.Accounts;

public class AccountByIdQuery : IRequest<Result<IQueryable<Account>>>
{

}
