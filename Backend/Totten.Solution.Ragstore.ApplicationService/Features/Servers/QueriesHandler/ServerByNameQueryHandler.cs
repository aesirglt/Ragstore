namespace Totten.Solution.Ragstore.ApplicationService.Features.Servers.QueriesHandler;

using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Queries;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class ServerByNameQueryHandler(IServerRepository repository) : IRequestHandler<ServerByNameQuery, Result<Server>>
{
    private readonly IServerRepository _repository = repository;

    public Task<Result<Server>> Handle(ServerByNameQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetByName(request.Name)
                          .Match(server => server,
                                () => Result.Of<Server>((NotFoundError)""))
                          .AsTask();
    }
}
