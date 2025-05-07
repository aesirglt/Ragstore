namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Vendings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreCollectionQuery : IRequest<Result<IQueryable<VendingStore>>>
{
    public int? ItemId { get; set; } = null;
}
