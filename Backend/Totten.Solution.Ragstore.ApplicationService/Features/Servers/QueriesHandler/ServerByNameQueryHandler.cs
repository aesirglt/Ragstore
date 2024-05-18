namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ServerByNameQueryHandler : IRequestHandler<ServerByNameQuery, Result<Exception, Server>>
{
    private IServerRepository _repository;
    public ServerByNameQueryHandler(IServerRepository repository)
    {
        _repository = repository;
    }
    public Task<Result<Exception, Server>> Handle(ServerByNameQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetByName(request.Name).AsTask();
    }
}
