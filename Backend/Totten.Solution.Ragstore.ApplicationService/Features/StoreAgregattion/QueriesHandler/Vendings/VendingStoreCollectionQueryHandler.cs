namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler.Vendings;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries.Vendings;
using Totten.Solution.Ragstore.ApplicationService.Notifications.ODataFilters;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreCollectionQueryHandler(
    IMediator mediator,
    IVendingStoreRepository storeRepository) : IRequestHandler<VendingStoreCollectionQuery, Result<IQueryable<VendingStore>>>
{
    private readonly IMediator _mediator = mediator;
    private readonly IVendingStoreRepository _storeRepository = storeRepository;

    public async Task<Result<IQueryable<VendingStore>>> Handle(VendingStoreCollectionQuery request, CancellationToken cancellationToken)
    {
        IQueryable<VendingStore> query =
            _storeRepository.GetAll(x => x.VendingStoreItems.Any(v => request.ItemId == null || v.ItemId == request.ItemId));

         if (request.ItemId != null)
            await _mediator.Publish(new ODataFilterNotification
            {
                Type = nameof(VendingStore),
                ItemId = request.ItemId ?? 0,
            });

        return Result.Of(query);
    }
}
