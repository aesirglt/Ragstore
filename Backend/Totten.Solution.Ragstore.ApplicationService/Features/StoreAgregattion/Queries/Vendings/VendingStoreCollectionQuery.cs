namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.Vendings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreCollectionQuery : IRequest<Result<IQueryable<VendingStore>>>
{
    public int? ItemId { get; set; } = null;
}
