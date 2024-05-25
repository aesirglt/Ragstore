namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;

public class VendingStoreItemsCollectionQuery : IRequest<Result<IQueryable<StoreItemResponseModel>>>
{
    public string ItemName { get; set; } = "";
}
