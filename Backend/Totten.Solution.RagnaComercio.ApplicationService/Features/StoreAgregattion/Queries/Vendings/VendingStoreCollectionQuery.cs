namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Vendings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;

public class VendingStoreCollectionQuery : IRequest<Result<IQueryable<StoreResumeViewModel>>>
{
    public int? ItemId { get; set; } = null;
}
