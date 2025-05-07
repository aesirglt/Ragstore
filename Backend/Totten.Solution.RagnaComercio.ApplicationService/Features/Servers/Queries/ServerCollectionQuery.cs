namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public class ServerCollectionQuery : IRequest<Result<IQueryable<Server>>>
{
}
