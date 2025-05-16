namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.CommandsHandler;

using AutoMapper;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.Notifications.Stores;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using static Totten.Solution.RagnaComercio.ApplicationService.Notifications.Stores.NewStoreNotification;

public class VendingStoreSaveCommandHandler(
    IMediator mediator,
    IMapper mapper,
    IVendingStoreRepository storeRepository,
    IVendingStoreItemRepository vendingStoreItemRepository)
    : IRequestHandler<VendingStoreSaveCommand, Result<Success>>
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly IVendingStoreRepository _storeRepository = storeRepository;
    private readonly IVendingStoreItemRepository _vendingStoreItemRepository = vendingStoreItemRepository;

    public async Task<Result<Success>> Handle(
        VendingStoreSaveCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _storeRepository
                    .GetByCharacterId(request.CharacterId)
                    .Match(storeInDb => UpdateFlow(request, storeInDb),
                           () => SaveFlow(request));

            _ = _mediator.Publish(new NewStoreNotification
            {
                Server = "",
                StoreId = result.Id,
                Where = $"{request.Map} {request.Location}",
                Merchant = request.CharacterName,
                StoreType = nameof(VendingStore),
                Date = DateTime.UtcNow,
                Items =
                    [.. request.StoreItems
                        .Select(x => new NewStoreNotificationItem()
                        {
                            ItemId = x.ItemId,
                            ItemPrice = x.Price
                        })]
            }, CancellationToken.None);

            return Result.Success;
        }
        catch (Exception ex)
        {
            return UnhandledError.New("Erro ao salvar uma nova loja", ex);
        }
    }

    private async Task<VendingStore> SaveFlow(VendingStoreSaveCommand request)
    {
        var store = _mapper.Map<VendingStore>(request);

        await _storeRepository.Save(store with
        {
            VendingStoreItems = MapStoreItem(request, store)
        });

        return store;
    }

    private async Task<VendingStore> UpdateFlow(VendingStoreSaveCommand request, VendingStore storeInDb)
    {
        storeInDb = Map(request, storeInDb);
        await _storeRepository.Update(storeInDb);

        _ = await _vendingStoreItemRepository.DeleteAll(storeInDb.Id);

        foreach (var vending in storeInDb.VendingStoreItems)
        {
            await _vendingStoreItemRepository.Save(vending);
        }

        return storeInDb;
    }

    private VendingStore Map(VendingStoreSaveCommand request, VendingStore vendingStore)
    {
        var storeId = vendingStore.Id;
        _mapper.Map(request, vendingStore);

        return vendingStore with
        {
            Id = storeId,
            VendingStoreItems = MapStoreItem(request, vendingStore)
        };
    }

    private static List<VendingStoreItem> MapStoreItem(VendingStoreSaveCommand request, VendingStore store)
        => store.VendingStoreItems
                .Select(item => item with
                {
                    CharacterId = store.CharacterId,
                    AccountId = store.AccountId,
                    Map = $"{store.Map} {store.Location}",
                    StoreName = request.Name,
                    CharacterName = request.CharacterName
                }).ToList();
}
