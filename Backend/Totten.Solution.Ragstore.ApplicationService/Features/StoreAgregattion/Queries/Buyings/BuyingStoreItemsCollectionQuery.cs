namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.Buyings;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;

public class BuyingStoreItemsCollectionQuery : IRequest<Result<IQueryable<StoreItemResponseModel>>>
{
    public string ItemName { get; set; } = "";
}
