namespace Totten.Solution.Ragstore.ApplicationService.Features.Accounts.CommandsHandler;
using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Accounts.Commands;

public class AccountCreateHandler : IRequestHandler<AccountCreateCommand, Result<Success>>
{
    public Task<Result<Success>> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
