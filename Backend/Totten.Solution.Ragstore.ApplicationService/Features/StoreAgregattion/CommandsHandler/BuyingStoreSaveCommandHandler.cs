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
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using static Totten.Solution.Ragstore.ApplicationService.Notifications.Stores.NewStoreNotification;


public class BuyingStoreSaveCommandHandler(
    IMediator mediator,
    IMapper mapper,
    IBuyingStoreRepository storeRepository,
    IBuyingStoreItemRepository storeItemRepository)
    : IRequestHandler<BuyingStoreSaveCommand, Result<Success>>
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly IBuyingStoreRepository _storeRepository = storeRepository;
    private readonly IBuyingStoreItemRepository _storeItemRepository = storeItemRepository;

    public async Task<Result<Success>> Handle(BuyingStoreSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var flowByBuying = _storeRepository.GetByCharacterId(request.CharacterId)
                .MatchAsync(storeInDb => UpdateFlow(request, storeInDb), () => SaveFlow(request));

            _ = _mediator.Publish(new NewStoreNotification
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
            }, CancellationToken.None);

            return await flowByBuying;
        }
        catch (Exception ex)
        {
            UnhandledError error = ("Erro ao salvar uma nova loja", ex);
            return error;
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
    private Task<Success> SaveFlow(BuyingStoreSaveCommand request)
    {
        var mappedStore = _mapper.Map<BuyingStore>(request);
        var store = mappedStore with { BuyingStoreItem = MapBuyingItem(request, mappedStore) };
        return _storeRepository.Save(store);
    }

    private async Task<Success> UpdateFlow(BuyingStoreSaveCommand request, BuyingStore storeInDb)
    {
        _ = await _storeItemRepository.DeleteAll(storeInDb.Id);
        _ = await _storeRepository.Remove(storeInDb);

        return await SaveFlow(request);
    }
}
