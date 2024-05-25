namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreItemsCollectionQueryHandler : IRequestHandler<VendingStoreItemsCollectionQuery, Result<IQueryable<StoreItemResponseModel>>>
{
    private IVendingStoreItemRepository _vendingStoreItemRepository;

    public VendingStoreItemsCollectionQueryHandler(IVendingStoreItemRepository vendingStoreItemRepository)
        => _vendingStoreItemRepository = vendingStoreItemRepository;

    public async Task<Result<IQueryable<StoreItemResponseModel>>> Handle(VendingStoreItemsCollectionQuery request, CancellationToken cancellationToken)
    {
        var storeItem = await _vendingStoreItemRepository
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
            }).AsTask();

        return new(storeItem);
    }
}
