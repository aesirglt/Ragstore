namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class BuyingStoreItemsCollectionQueryHandler(IBuyingStoreItemRepository storeItemRepository) : IRequestHandler<BuyingStoreItemsCollectionQuery, Result<IQueryable<StoreItemResponseModel>>>
{
    private readonly IBuyingStoreItemRepository _storeItemRepository = storeItemRepository;

    public async Task<Result<IQueryable<StoreItemResponseModel>>> Handle(BuyingStoreItemsCollectionQuery request, CancellationToken cancellationToken)
    {
        var storeItems = await _storeItemRepository
            .GetAllByItemName(request.ItemName)
            .Select(item => new StoreItemResponseModel
            {
                Id = item.ItemId,
                StoreId = item.StoreId,
                ItemName = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                VendingType = nameof(VendingStoreItem),
                StoreName = item.StoreName,
                Map = item.Map,
                CharacterName = item.CharacterName
            })
            .AsTask();

        return Result.Of(storeItems);
    }
}
