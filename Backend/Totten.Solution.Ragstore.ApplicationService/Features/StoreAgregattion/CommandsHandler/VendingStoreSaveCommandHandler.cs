namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.CommandsHandler;

using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stories;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using static Totten.Solution.Ragstore.ApplicationService.Notifications.Stories.NewStoreNotification;
using Unit = Infra.Cross.Functionals.Unit;

public class VendingStoreSaveCommandHandler : IRequestHandler<VendingStoreSaveCommand, Result<Exception, Unit>>
{
    private IMediator _mediator;
    private IMapper _mapper;
    private IVendingStoreRepository _storeRepository;
    private IVendingStoreItemRepository _vendingStoreItemRepository;

    public VendingStoreSaveCommandHandler(
        IMediator mediator,
        IMapper mapper,
        IVendingStoreRepository storeRepository,
        IVendingStoreItemRepository vendingStoreItemRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _storeRepository = storeRepository;
        _vendingStoreItemRepository = vendingStoreItemRepository;
    }
    public async Task<Result<Exception, Unit>> Handle(VendingStoreSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var flowByVending = _storeRepository.GetByCharacterId(request.CharacterId) switch
            {
                null => SaveFlow(request),
                var storeInDb => UpdateFlow(request, storeInDb)
            };

            _ = Publish(new NewStoreNotification
            {
                Server = request.Server,
                Where = $"{request.Map} {request.Location}",
                Merchant = request.CharacterName,
                Date = DateTime.Now,
                Items = request.VendingStoreItems.Select(x => new NewStoreNotificationItem()
                {
                    ItemId = x.ItemId,
                    ItemValue = x.Price
                }).ToList()
            });

            return await flowByVending;
        }
        catch (Exception ex)
        {
            return new InternalError("Erro ao salvar uma nova loja", ex);
        }
    }

    private Task<Unit> SaveFlow(VendingStoreSaveCommand request)
    {
        var store = _mapper.Map<VendingStore>(request);
        store.VendingStoreItems = MapStoreItem(request, store);
        return _storeRepository.Save(store);
    }


    private async Task<Unit> UpdateFlow(VendingStoreSaveCommand request, VendingStore storeInDb)
    {
        storeInDb = Map(request, storeInDb);
        await _storeRepository.Update(storeInDb);

        _ = await _vendingStoreItemRepository.DeleteAll(storeInDb.Id);

        foreach (var vending in storeInDb.VendingStoreItems)
        {
            await _vendingStoreItemRepository.Save(vending);
        }

        return new Unit();
    }

    private Task Publish(NewStoreNotification newStoreNotification)
        => _mediator.Publish(newStoreNotification);

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
