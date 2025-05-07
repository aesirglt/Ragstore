namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler.Buyings;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Buyings;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class BuyingStoreItemsCollectionQueryHandler(IBuyingStoreItemRepository storeItemRepository) : IRequestHandler<BuyingStoreItemsCollectionQuery, Result<IQueryable<StoreItemResponseModel>>>
{
    private readonly IBuyingStoreItemRepository _storeItemRepository = storeItemRepository;

    public async Task<Result<IQueryable<StoreItemResponseModel>>> Handle(BuyingStoreItemsCollectionQuery request, CancellationToken cancellationToken)
    {
        var storeItems = await _storeItemRepository
            .GetAllByItemName(request.ItemName)
            .Select(item => new StoreItemResponseModel
            {
                ItemId = item.ItemId,
                ItemName = item.Name,
                Quantity = item.Quantity,
                Category = $"{item.Type}",
                Image = "",
            })
            .AsTask();

        return Result.Of(storeItems);
    }
}
