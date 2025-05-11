namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class StoreItemResumeQueryHandler(
    IVendingStoreItemRepository vendingStoreItemRepository,
    IBuyingStoreItemRepository buyingStore)
    : IRequestHandler<StoreItemResumeQuery, Result<IQueryable<StoreItemResumeViewModel>>>
{
    private readonly IVendingStoreItemRepository _vendingRepositore = vendingStoreItemRepository;
    private readonly IBuyingStoreItemRepository _buyingRepositore = buyingStore;

    public async Task<Result<IQueryable<StoreItemResumeViewModel>>> Handle(StoreItemResumeQuery request, CancellationToken cancellationToken)
    {
        return request.StoreType == nameof(VendingStore)
               ? await ExecuteCmd(_vendingRepositore)
               : await ExecuteCmd(_buyingRepositore);
    }

    public async Task<Result<IQueryable<StoreItemResumeViewModel>>> ExecuteCmd<TStoreItem>(
        IStoreRepository<TStoreItem> repository)
        where TStoreItem : StoreItem<TStoreItem>
    {
        var result = repository
            .GetAll()
            .GroupBy(item => new
            {
                item.ItemId,
                item.Name,
                item.Type
            })
            .Select(group => new StoreItemResumeViewModel
            {
                ItemId = group.Key.ItemId,
                ItemName = group.Key.Name,
                Category = group.Key.Type.ToString(),
                Quantity = group.Sum(i => i.Quantity),
                Image = "https://static.divine-pride.net/images/items/item/" + group.Key.ItemId + ".png",
            });

        return Result.Of(await result.AsTask());
    }
}
