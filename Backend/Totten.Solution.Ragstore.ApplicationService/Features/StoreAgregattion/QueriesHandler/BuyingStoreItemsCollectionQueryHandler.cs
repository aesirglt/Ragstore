namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class BuyingStoreItemsCollectionQueryHandler : IRequestHandler<BuyingStoreItemsCollectionQuery, Result<Exception, IQueryable<StoreItemResponseModel>>>
{
    private IBuyingStoreItemRepository _storeItemRepository;

    public BuyingStoreItemsCollectionQueryHandler(IBuyingStoreItemRepository storeItemRepository)
        => _storeItemRepository = storeItemRepository;

    public async Task<Result<Exception, IQueryable<StoreItemResponseModel>>> Handle(BuyingStoreItemsCollectionQuery request, CancellationToken cancellationToken)
    {
        var storeItem = _storeItemRepository
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
            });

        return await
            Result<Exception, IQueryable<StoreItemResponseModel>>
            .Ok(storeItem)
            .AsTask();
    }
}
