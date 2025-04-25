namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Statics;
using static Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.StoreItemValueSumaryQuery;

public class StoreItemsCollectionQueryHandler(
    IVendingStoreItemRepository vendingStoreItemRepository,
    IBuyingStoreItemRepository buyingStore)
    : IRequestHandler<StoreItemsCollectionQuery, Result<IQueryable<StoreItemResponseModel>>>
{
    private readonly IVendingStoreItemRepository _vendingRepositore = vendingStoreItemRepository;
    private readonly IBuyingStoreItemRepository _buyingRepositore = buyingStore;

    public async Task<Result<IQueryable<StoreItemResponseModel>>> Handle(StoreItemsCollectionQuery request, CancellationToken cancellationToken)
    {
        return request.StoreType == EStoreItemStoreType.Vending
               ? await ExecuteCmd(_vendingRepositore)
               : await ExecuteCmd(_buyingRepositore);
    }

    public async Task<Result<IQueryable<StoreItemResponseModel>>> ExecuteCmd<TStoreItem>(IStoreRepository<TStoreItem> repository)
        where TStoreItem : StoreItem<TStoreItem>
    {
        var result = repository
            .GetAll()
            .GroupBy(item => new
            {
                item.ItemId,
                item.Name,
                item.Type,
                item.Price
            })
            .Select(group => new StoreItemResponseModel
            {
                ItemId = group.Key.ItemId,
                ItemName = group.Key.Name,
                Price = group.Key.Price,
                Category = group.Key.Type.ToString(),
                Quantity = group.Sum(i => i.Quantity),
                Image = "https://static.divine-pride.net/images/items/item/" + group.Key.ItemId + ".png"
            });

        return Result.Of(await result.AsTask());
    }
}
