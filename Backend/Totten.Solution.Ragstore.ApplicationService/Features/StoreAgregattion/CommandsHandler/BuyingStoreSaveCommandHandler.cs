namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.CommandsHandler;

using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stores;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using static Totten.Solution.Ragstore.ApplicationService.Notifications.Stores.NewStoreNotification;
using Unit = Infra.Cross.Functionals.Unit;

public class BuyingStoreSaveCommandHandler : IRequestHandler<BuyingStoreSaveCommand, Result<Exception, Unit>>
{
    private IMediator _mediator;
    private IMapper _mapper;
    private IBuyingStoreRepository _storeRepository;
    private IBuyingStoreItemRepository _storeItemRepository;

    public BuyingStoreSaveCommandHandler(
        IMediator mediator,
        IMapper mapper,
        IBuyingStoreRepository storeRepository,
        IBuyingStoreItemRepository storeItemRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _storeRepository = storeRepository;
        _storeItemRepository = storeItemRepository;
    }

    public async Task<Result<Exception, Unit>> Handle(BuyingStoreSaveCommand request, CancellationToken cancellationToken)
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
                StoreType = nameof(BuyingStore),
                Merchant = request.CharacterName,
                Date = DateTime.Now,
                Items = request.StoreItems.Select(x => new NewStoreNotificationItem()
                {
                    ItemId = x.ItemId,
                    ItemPrice = x.Price
                }).ToList()
            });

            return await flowByVending;
        }
        catch (Exception ex)
        {
            return new InternalError("Erro ao salvar uma nova loja", ex);
        }
    }

    private BuyingStoreItem MapBuyingItem(BuyingStoreSaveCommand request, BuyingStore store)
    {
        var buyingStoreItem = _mapper.Map<BuyingStoreItem>(request.StoreItems.FirstOrDefault());
        buyingStoreItem.CharacterName = request.CharacterName;
        buyingStoreItem.Map = $"{store.Map} {store.Location}";
        buyingStoreItem.StoreName = request.Name;

        return buyingStoreItem;
    }
    private Task<Unit> SaveFlow(BuyingStoreSaveCommand request)
    {
        var mappedStore = _mapper.Map<BuyingStore>(request);
        var store = mappedStore with { BuyingStoreItem = MapBuyingItem(request, mappedStore) };
        return _storeRepository.Save(store);
    }

    private async Task<Unit> UpdateFlow(BuyingStoreSaveCommand request, BuyingStore storeInDb)
    {
        _ = await _storeItemRepository.DeleteAll(storeInDb.Id);
        _ = await _storeRepository.Remove(storeInDb);

        return await SaveFlow(request);
    }

    private Task Publish(NewStoreNotification newStoreNotification)
        => _mediator.Publish(newStoreNotification);

}
