namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.QueriesHandler;

using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Queries;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class ServerCollectionQueryHandler(IServerRepository serverRepository) : IRequestHandler<ServerCollectionQuery, Result<IQueryable<Server>>>
{
    private readonly IServerRepository _serverRepository = serverRepository;

    public async Task<Result<IQueryable<Server>>> Handle(
        ServerCollectionQuery _,
        CancellationToken cancellationToken)
    {
        try
        {
            return await Result.Of(_serverRepository.GetAll()).AsTask();
        }
        catch (Exception ex)
        {
            UnhandledError error = ("Erro ao salvar um callback", ex);
            return error;
        }
    }
}
