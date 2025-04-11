namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.QueriesHandler;
using FunctionalConcepts.Results;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Users.Queries;
using Totten.Solution.Ragstore.Domain.Features.Users;

public class UserByIdHandler : IRequestHandler<UserByIdQuery, Result<User>>
{
    public Task<Result<User>> Handle(UserByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
