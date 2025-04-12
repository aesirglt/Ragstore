namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.Users;

public class UserByIdQuery : IRequest<Result<User>>
{
}
