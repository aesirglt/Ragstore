namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.CommandsHandler;
using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Users.Commands;

public class UserCreateHandler : IRequestHandler<UserCreateCommand, Result<Success>>
{
    public Task<Result<Success>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
