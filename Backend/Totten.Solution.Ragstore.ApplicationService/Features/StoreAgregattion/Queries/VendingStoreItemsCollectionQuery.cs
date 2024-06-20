namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;

public class VendingStoreItemsCollectionQuery : IRequest<Result<IQueryable<StoreItemResponseModel>>>
{
    public string ItemName { get; set; } = "";
}
