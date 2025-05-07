namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Buyings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;

public class BuyingStoreByIdQuery : IRequest<Result<BuyingStore>>
{
    public int Id { get; set; }
}
