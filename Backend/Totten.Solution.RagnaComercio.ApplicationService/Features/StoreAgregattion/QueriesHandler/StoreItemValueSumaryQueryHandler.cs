namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;
using static Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.StoreItemValueSumaryQuery;

public class StoreItemValueSumaryQueryHandler(
    IVendingStoreItemRepository vendingStore,
    IBuyingStoreItemRepository buyingStore)
    : IRequestHandler<StoreItemValueSumaryQuery, Result<StoreItemValueSumaryResponseModel>>
{
    private readonly IVendingStoreItemRepository _vendingRepositore = vendingStore;
    private readonly IBuyingStoreItemRepository _buyingRepositore = buyingStore;

    public async Task<Result<StoreItemValueSumaryResponseModel>> Handle(
        StoreItemValueSumaryQuery request,
        CancellationToken cancellationToken)
    {
        return request.StoreType == EStoreItemStoreType.Vending
               ? await ExecuteCmd(request.ItemId, _vendingRepositore)
               : await ExecuteCmd(request.ItemId, _buyingRepositore);
    }

    private static async Task<Result<StoreItemValueSumaryResponseModel>> ExecuteCmd<TStoreItem>(int itemId, IStoreRepository<TStoreItem> repository)
        where TStoreItem : StoreItem<TStoreItem>
    {
        var initMonthDate =
            DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-01T00:00:00Z"),
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

        //var currentDate = DateTime.UtcNow.Date;

        var allItemsById =
            repository.GetAll(x => x.ItemId == itemId)
            .Select(s => new
            {
                s.Name,
                s.Price,
                s.UpdatedAt
            });

        var currentStores = allItemsById/*.Where(x => x.UpdatedAt >= currentDate)*/
                                     .Select(s => s.Price);

        var itemsOnThisMonth = allItemsById
                                        .Where(x => x.UpdatedAt >= initMonthDate && x.UpdatedAt <= DateTime.UtcNow)
                                        .Select(s => s.Price)
                                        .OrderBy(price => price);

        return await Result.Of(new StoreItemValueSumaryResponseModel
        {
            ItemName = allItemsById.FirstOrDefault()?.Name ?? "Desconhecido",
            ImageUrl = $"https://static.divine-pride.net/images/items/item/{itemId}.png",
            CurrentMinValue = currentStores.OrderBy(p => p).FirstOrDefault(),
            CurrentMaxValue = currentStores.OrderByDescending(p => p).FirstOrDefault(),
            MinValue = itemsOnThisMonth.FirstOrDefault(),
            MaxValue = itemsOnThisMonth.Reverse().FirstOrDefault(),
            Average = itemsOnThisMonth.Any() ? itemsOnThisMonth.Average() : 0,
            StoreNumbers = currentStores.LongCount()
        }).AsTask();
    }
}
