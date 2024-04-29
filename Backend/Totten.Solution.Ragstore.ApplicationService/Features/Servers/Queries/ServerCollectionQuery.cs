namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ServerCollectionQuery : IRequest<Result<Exception, IQueryable<Server>>>
{
}
