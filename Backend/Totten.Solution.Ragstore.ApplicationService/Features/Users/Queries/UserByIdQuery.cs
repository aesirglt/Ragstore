namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.Users;

public class UserByIdQuery : IRequest<Result<IQueryable<User>>>
{
}
