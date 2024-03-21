namespace Totten.Solution.Ragstore.ApplicationService.Features.Stores.QueriesCommand;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreByIdQuery : IRequest<Result<Exception, VendingStore>>
{
    public Guid Id { get; set; }

    public StoreByIdQuery(Guid id)
    {
        Id = id;
    }
}
