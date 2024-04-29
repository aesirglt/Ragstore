namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.QueriesHandler;

using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ServerCollectionQueryHandler : IRequestHandler<ServerCollectionQuery, Result<Exception, IQueryable<Server>>>
{
    private IServerRepository _serverRepository;

    public ServerCollectionQueryHandler(IServerRepository serverRepository)
    {
        _serverRepository = serverRepository;
    }

    public async Task<Result<Exception, IQueryable<Server>>> Handle(ServerCollectionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var servers = _serverRepository.GetAll();
            var result = Result<Exception, IQueryable<Server>>.Ok(servers);

            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
