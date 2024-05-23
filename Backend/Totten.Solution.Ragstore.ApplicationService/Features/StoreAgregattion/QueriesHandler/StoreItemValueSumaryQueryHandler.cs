namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

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
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using static Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.StoreItemValueSumaryQuery;

public class StoreItemValueSumaryQueryHandler : IRequestHandler<StoreItemValueSumaryQuery, Result<Exception, StoreItemValueSumaryResponseModel>>
{
    private readonly IServerRepository _serverRepository;
    private readonly IVendingStoreItemRepository _vendingRepositore;
    private readonly IBuyingStoreItemRepository _buyingRepositore;

    public StoreItemValueSumaryQueryHandler(
        IServerRepository serverRepository, IVendingStoreItemRepository vendingStore,
        IBuyingStoreItemRepository buyingStore)
    {
        _serverRepository = serverRepository;
        _vendingRepositore = vendingStore;
        _buyingRepositore = buyingStore;
    }

    public async Task<Result<Exception, StoreItemValueSumaryResponseModel>> Handle(
        StoreItemValueSumaryQuery request,
        CancellationToken cancellationToken)
    {
        return await _serverRepository.GetByName(request.Server).Match(async server =>
        {
            var action = Choice(server);
            return await action();
        }, OnFail);

        Func<Task<Result<Exception, StoreItemValueSumaryResponseModel>>> Choice(Server server)
            => request.StoreType == EStoreItemStoreType.Vending
                        ? () => ExecuteCmd(server, request.ItemId, _vendingRepositore)
                        : () => ExecuteCmd(server, request.ItemId, _buyingRepositore);
    }

    private async Task<Result<Exception, StoreItemValueSumaryResponseModel>> ExecuteCmd<TStoreItem>(
        Server server, int itemId, IStoreRepository<TStoreItem> repository)
        where TStoreItem : StoreItem<TStoreItem>
    {
        var itemsOnStores = repository.GetAllByFilter(x => x.UpdatedAt >= server.UpdatedAt && x.ItemId == itemId)
                                     .Select(s => s.Price)
                                     .ToArray();

        var itemsOnThisMonth = repository.GetAllByFilter(x => x.ItemId == itemId)
                                        .AsEnumerable()
                                        .Where(x => x.UpdatedAt.Month == DateTime.Now.Month && x.UpdatedAt.Year == DateTime.Now.Year)
                                        .Select(s => s.Price)
                                        .OrderBy(price => price)
                                        .ToArray();

        return await Task.FromResult(new StoreItemValueSumaryResponseModel
        {
            CurrentMinValue = itemsOnStores.MinBy(p => p),
            CurrentMaxValue = itemsOnStores.MaxBy(p => p),
            MinValue = itemsOnThisMonth.MinBy(s => s),
            Average = itemsOnThisMonth.Average(),
            StoreNumbers = itemsOnStores.Count()
        });
    }

    private Task<Result<Exception, StoreItemValueSumaryResponseModel>> OnFail(Exception exception)
        => Task.FromResult(Result<Exception, StoreItemValueSumaryResponseModel>.Err(exception));

}
