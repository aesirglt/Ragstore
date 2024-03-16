namespace Totten.Solution.Ragstore.ApplicationService.Features.Stores.QueriesCommand;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreByIdQuery : IRequest<Result<Exception, Store>>
{
    public string Id { get; set; } = string.Empty;
}
