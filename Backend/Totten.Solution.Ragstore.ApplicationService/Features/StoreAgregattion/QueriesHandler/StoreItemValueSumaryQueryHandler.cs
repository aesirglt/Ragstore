namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
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
        var maybeServer = _serverRepository.GetByName(request.Server);
        return await maybeServer.MatchAsync(async server =>
        {
            var action = Choice(server);
            return await action();
        }, () => (NotFoundError)"Server not found");

        Func<Task<Result<StoreItemValueSumaryResponseModel>>> Choice(Server server)
            => request.StoreType == EStoreItemStoreType.Vending
                        ? () => ExecuteCmd(server, request.ItemId, _vendingRepositore)
                        : () => ExecuteCmd(server, request.ItemId, _buyingRepositore);
    }

    private static async Task<Result<StoreItemValueSumaryResponseModel>> ExecuteCmd<TStoreItem>(
        Server server, int itemId, IStoreRepository<TStoreItem> repository)
        where TStoreItem : StoreItem<TStoreItem>
    {
        var itemsOnStores = repository.GetAll(x => x.UpdatedAt >= server.UpdatedAt && x.ItemId == itemId)
                                     .Select(s => s.Price)
                                     .ToArray();

        var itemsOnThisMonth = repository.GetAll(x => x.ItemId == itemId)
                                        .AsEnumerable()
                                        .Where(x => x.UpdatedAt.Month == DateTime.Now.Month && x.UpdatedAt.Year == DateTime.Now.Year)
                                        .Select(s => s.Price)
                                        .OrderBy(price => price)
                                        .ToArray();

        return await Result.Of(new StoreItemValueSumaryResponseModel
        {
            CurrentMinValue = itemsOnStores.MinBy(p => p),
            CurrentMaxValue = itemsOnStores.MaxBy(p => p),
            MinValue = itemsOnThisMonth.MinBy(s => s),
            Average = itemsOnThisMonth.Average(),
            StoreNumbers = itemsOnStores.Length
        }).AsTask();
    }

}
