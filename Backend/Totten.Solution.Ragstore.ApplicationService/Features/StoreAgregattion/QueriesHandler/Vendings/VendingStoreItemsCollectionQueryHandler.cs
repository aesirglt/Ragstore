namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler.Vendings;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.Vendings;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class VendingStoreItemsCollectionQueryHandler(IVendingStoreItemRepository vendingStoreItemRepository)
    : IRequestHandler<VendingStoreItemsCollectionQuery, Result<IQueryable<StoreItemResponseModel>>>
{
    private readonly IVendingStoreItemRepository _vendingStoreItemRepository = vendingStoreItemRepository;

    public async Task<Result<IQueryable<StoreItemResponseModel>>> Handle(VendingStoreItemsCollectionQuery request, CancellationToken cancellationToken)
    {
        var result = _vendingStoreItemRepository
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
