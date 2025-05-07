namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler.Vendings;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Vendings;
using Totten.Solution.RagnaComercio.ApplicationService.Notifications.ODataFilters;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreCollectionQueryHandler(
    IMediator mediator,
    IVendingStoreRepository storeRepository) : IRequestHandler<VendingStoreCollectionQuery, Result<IQueryable<StoreResumeViewModel>>>
{
    private readonly IMediator _mediator = mediator;
    private readonly IVendingStoreRepository _storeRepository = storeRepository;

    public async Task<Result<IQueryable<StoreResumeViewModel>>> Handle(VendingStoreCollectionQuery request, CancellationToken cancellationToken)
    {
        IQueryable<StoreResumeViewModel> query =
            _storeRepository.GetAll(x => x.VendingStoreItems.Any(v => request.ItemId == null || v.ItemId == request.ItemId))
            .Select(s => new StoreResumeViewModel
            {
                AccountId = s.AccountId,
                CharacterId = s.CharacterId,
                CharacterName = s.Character == null ? "" : s.Character.Name,
                Name = s.Name,
                ExpireDate = s.ExpireDate,
                Id = s.Id,
                Location = s.Location,
                Map = s.Map,
                ItemPrice = s.VendingStoreItems.Count > 0 ? (int)s.VendingStoreItems.FirstOrDefault(x => x.ItemId == request.ItemId)!.Price : 0
            });

         if (request.ItemId != null)
            await _mediator.Publish(new ODataFilterNotification
            {
                Type = nameof(VendingStore),
                ItemId = request.ItemId ?? 0,
            });

        return Result.Of(query);
    }
}
