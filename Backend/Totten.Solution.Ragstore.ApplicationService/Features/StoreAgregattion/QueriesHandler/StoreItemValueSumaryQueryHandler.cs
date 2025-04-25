namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Statics;
using static Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.StoreItemValueSumaryQuery;

public class StoreItemValueSumaryQueryHandler(
    IServerRepository serverRepository, IVendingStoreItemRepository vendingStore,
    IBuyingStoreItemRepository buyingStore)
    : IRequestHandler<StoreItemValueSumaryQuery, Result<StoreItemValueSumaryResponseModel>>
{
    private readonly IServerRepository _serverRepository = serverRepository;
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

    private static async Task<Result<StoreItemValueSumaryResponseModel>> ExecuteCmd<TStoreItem>(int[] itemIds, IStoreRepository<TStoreItem> repository)
        where TStoreItem : StoreItem<TStoreItem>
    {
        var initMonthDate =
            DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-01T00:00:00Z"),
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

        var currentDate = DateTime.UtcNow.Date;

        var allItemsById =
            repository.GetAll(x => itemIds.Any(id => x.ItemId == id))
            .Select(s => new
            {
                s.Price,
                s.UpdatedAt
            });

        var currentStores = allItemsById.Where(x => x.UpdatedAt >= currentDate)
                                     .Select(s => s.Price);

        var itemsOnThisMonth = allItemsById
                                        .Where(x => x.UpdatedAt >= initMonthDate && x.UpdatedAt <= DateTime.UtcNow)
                                        .Select(s => s.Price)
                                        .OrderBy(price => price);

        return await Result.Of(new StoreItemValueSumaryResponseModel
        {
            CurrentMinValue = currentStores.OrderBy(p => p).FirstOrDefault(),
            CurrentMaxValue = currentStores.OrderByDescending(p => p).FirstOrDefault(),
            MinValue = itemsOnThisMonth.OrderBy(p => p).FirstOrDefault(),
            MaxValue = itemsOnThisMonth.OrderByDescending(p => p).FirstOrDefault(),
            Average = itemsOnThisMonth.Average(),
            StoreNumbers = currentStores.LongCount()
        }).AsTask();
    }
}
