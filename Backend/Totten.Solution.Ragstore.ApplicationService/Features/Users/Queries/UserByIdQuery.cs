namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.Queries;

using MediatR;
using System;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.Users;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class UserByIdQuery : IRequest<Result<Exception, IQueryable<User>>>
{
}
