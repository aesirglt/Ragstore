namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.RagnaComercio.ApplicationService.Notifications.ODataFilters;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class StoreItemCollectionQueryHandler(
    IMediator mediator,
    IVendingStoreItemRepository vendingRepositore,
    IBuyingStoreItemRepository buyingRepositore)
    : IRequestHandler<StoreItemCollectionQuery, Result<IQueryable<StoreResumeViewModel>>>
{
    private readonly IMediator _mediator = mediator;
    private readonly IVendingStoreItemRepository _vendingRepositore = vendingRepositore;
    private readonly IBuyingStoreItemRepository _buyingRepositore = buyingRepositore;

    public async Task<Result<IQueryable<StoreResumeViewModel>>> Handle(StoreItemCollectionQuery request, CancellationToken cancellationToken)
    {
        return request.StoreType == nameof(VendingStore)
               ? await Execute(request, _vendingRepositore, cancellationToken)
               : await Execute(request, _buyingRepositore, cancellationToken);
    }
    private async Task<Result<IQueryable<StoreResumeViewModel>>> Execute<TStoreItem>(
        StoreItemCollectionQuery request,
        IStoreRepository<TStoreItem> repository,
        CancellationToken cancellationToken)
        where TStoreItem : StoreItem<TStoreItem>
    {
        string storeType = request.StoreType.ToString() ?? nameof(VendingStore);
        IQueryable<StoreResumeViewModel> query =
            repository.GetAll()
            .Select(s => new StoreResumeViewModel
            {
                Id = s.Id,
                AccountId = s.AccountId,
                CharacterId = s.CharacterId,
                CharacterName = s.CharacterName,
                Name = s.Name,
                Location = s.Map,
                ItemId = s.ItemId,
                ItemPrice = s.Price,
                Quantity = s.Quantity,
            });

        //await _mediator.Publish(new ODataFilterNotification
        //{
        //    Type = storeType,
        //    ItemId = request.ItemId,
        //}, cancellationToken);

        return await Result.Of(query).AsTask(cancellationToken);
    }
}
