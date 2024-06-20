namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public class ServerCollectionQuery : IRequest<Result<IQueryable<Server>>>
{
}
