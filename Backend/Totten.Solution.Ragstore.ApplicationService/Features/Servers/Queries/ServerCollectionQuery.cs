namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public class ServerCollectionQuery : IRequest<Result<IQueryable<Server>>>
{
}
