namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Servers.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public class ServerByNameQuery : IRequest<Result<Server>>
{
    public string Name { get; set; } = string.Empty;
}
