namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.CommandsHandler;

using AutoMapper;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stores;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using static Totten.Solution.Ragstore.ApplicationService.Notifications.Stores.NewStoreNotification;


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

    public async Task<Result<Success>> Handle(VendingStoreSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var flowByVending = _storeRepository.GetByCharacterId(request.CharacterId).Match(storeInDb => UpdateFlow(request, storeInDb), () => SaveFlow(request));

            _ = _mediator.Publish(new NewStoreNotification
            {
                Server = request.Server,
                Where = $"{request.Map} {request.Location}",
                Merchant = request.CharacterName,
                StoreType = nameof(VendingStore),
                Date = DateTime.Now,
                Items = request.StoreItems.Select(x => new NewStoreNotificationItem()
                {
                    ItemId = x.ItemId,
                    ItemPrice = x.Price
                }).ToList()
            }, CancellationToken.None);

            return await flowByVending;
        }
        catch (Exception ex)
        {
            UnhandledError error = ("Erro ao salvar uma nova loja", ex);
            return error;
        }
    }

    private Task<Success> SaveFlow(VendingStoreSaveCommand request)
    {
        var store = _mapper.Map<VendingStore>(request);
        store.VendingStoreItems = MapStoreItem(request, store);
        return _storeRepository.Save(store);
    }

    private async Task<Success> UpdateFlow(VendingStoreSaveCommand request, VendingStore storeInDb)
    {
        storeInDb = Map(request, storeInDb);
        await _storeRepository.Update(storeInDb);

        _ = await _vendingStoreItemRepository.DeleteAll(storeInDb.Id);

        foreach (var vending in storeInDb.VendingStoreItems)
        {
            await _vendingStoreItemRepository.Save(vending);
        }

        return Result.Success;
    }

    private VendingStore Map(VendingStoreSaveCommand request, VendingStore vendingStore)
    {
        var storeId = vendingStore.Id;
        _mapper.Map(request, vendingStore);

        vendingStore.VendingStoreItems = MapStoreItem(request, vendingStore);
        vendingStore.Id = storeId;
        return vendingStore;
    }

    private static List<VendingStoreItem> MapStoreItem(VendingStoreSaveCommand request, VendingStore store)
    {
        return store.VendingStoreItems
                    .Select(item => item with
                    {
                        Map = $"{store.Map} {store.Location}",
                        StoreName = request.Name,
                        CharacterName = request.CharacterName
                    }).ToList();
    }
}
