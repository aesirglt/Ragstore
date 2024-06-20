namespace Totten.Solution.Ragstore.ApplicationService.Features.Accounts.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class AccountCreateCommand : IRequest<Result<Success>>
{
}
