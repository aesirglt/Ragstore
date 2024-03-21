namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesCommand;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreByIdQuery : IRequest<Result<Exception, VendingStore>>
{
    public int Id { get; set; }

    public StoreByIdQuery(int id)
    {
        Id = id;
    }
}
