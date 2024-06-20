namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;

public class BuyingStoreItemsCollectionQuery : IRequest<Result<IQueryable<StoreItemResponseModel>>>
{
    public string ItemName { get; set; } = "";
}
