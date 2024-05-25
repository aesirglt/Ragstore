namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public class ServerCollectionQueryHandler : IRequestHandler<ServerCollectionQuery, Result<IQueryable<Server>>>
{
    private IServerRepository _serverRepository;

    public ServerCollectionQueryHandler(IServerRepository serverRepository)
    {
        _serverRepository = serverRepository;
    }

    public async Task<Result<IQueryable<Server>>> Handle(ServerCollectionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await new Result<IQueryable<Server>>(_serverRepository.GetAll()).AsTask();
        }
        catch (Exception ex)
        {
            return new Result<IQueryable<Server>>(ex);
        }
    }
}
