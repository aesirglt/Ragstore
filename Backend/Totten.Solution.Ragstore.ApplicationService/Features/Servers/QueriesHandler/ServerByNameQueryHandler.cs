namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;

public class ServerByNameQueryHandler : IRequestHandler<ServerByNameQuery, Result<Server>>
{
    private IServerRepository _repository;
    public ServerByNameQueryHandler(IServerRepository repository)
    {
        _repository = repository;
    }
    public Task<Result<Server>> Handle(ServerByNameQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetByName(request.Name)
                          .Match(server => new Result<Server>(server),
                                 () => new Result<Server>(new NotFoundError("")))
                          .AsTask();
    }
}
