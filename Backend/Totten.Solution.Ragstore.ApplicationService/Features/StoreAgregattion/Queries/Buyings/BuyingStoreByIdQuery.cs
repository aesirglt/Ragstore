namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.Buyings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;

public class BuyingStoreByIdQuery : IRequest<Result<BuyingStore>>
{
    public int Id { get; set; }
}
