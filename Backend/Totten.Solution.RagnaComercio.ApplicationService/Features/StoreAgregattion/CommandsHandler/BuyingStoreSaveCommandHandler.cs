﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.CommandsHandler;

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
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using static Totten.Solution.RagnaComercio.ApplicationService.Notifications.Stores.NewStoreNotification;

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
            var flowByBuying = await _storeRepository.GetByCharacterId(request.CharacterId)
                .MatchAsync(storeInDb => UpdateFlow(request, storeInDb), () => SaveFlow(request));

            _ = _mediator.Publish(new NewStoreNotification
            {
                Server = "",
                StoreName = request.Name,
                StoreId = flowByBuying.Id,
                Where = $"{request.Map} {request.Location}",
                StoreType = nameof(BuyingStore),
                Merchant = request.CharacterName,
                Date = DateTime.UtcNow,
                Items = [.. request.StoreItems.Select(x => new NewStoreNotificationItem()
                {
                    ItemId = x.ItemId,
                    ItemPrice = x.Price
                })]
            }, CancellationToken.None);

            return Result.Success;
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

    private async Task<BuyingStore> SaveFlow(BuyingStoreSaveCommand request)
    {
        var mappedStore = _mapper.Map<BuyingStore>(request);
        var store = mappedStore with { BuyingStoreItem = MapBuyingItem(request, mappedStore) };
        await _storeRepository.Save(store);
        return store;
    }

    private async Task<BuyingStore> UpdateFlow(BuyingStoreSaveCommand request, BuyingStore storeInDb)
    {
        _ = await _storeItemRepository.DeleteAll(storeInDb.Id);
        _ = await _storeRepository.Remove(storeInDb);

        return await SaveFlow(request);
    }
}
