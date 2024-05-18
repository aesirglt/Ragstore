namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class BuyingStoreByIdQuery : IRequest<Result<Exception, BuyingStore>>
{
    public int Id { get; set; }
}
