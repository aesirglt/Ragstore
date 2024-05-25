namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class BuyingStoreItemsCollectionQueryHandler : IRequestHandler<BuyingStoreItemsCollectionQuery, Result<IQueryable<StoreItemResponseModel>>>
{
    private IBuyingStoreItemRepository _storeItemRepository;

    public BuyingStoreItemsCollectionQueryHandler(IBuyingStoreItemRepository storeItemRepository)
        => _storeItemRepository = storeItemRepository;

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

        return new Result<IQueryable<StoreItemResponseModel>>(storeItems);
    }
}
