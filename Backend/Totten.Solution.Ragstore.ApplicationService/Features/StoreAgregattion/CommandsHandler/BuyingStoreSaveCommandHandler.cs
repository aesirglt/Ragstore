namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.CommandsHandler;

using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stores;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
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
                Merchant = request.CharacterName,
                Date = DateTime.Now,
                Items = request.VendingStoreItems.Select(x => new NewStoreNotificationItem()
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

    private Task<Unit> SaveFlow(BuyingStoreSaveCommand request)
    {
        var store = _mapper.Map<BuyingStore>(request);
        return _storeRepository.Save(store);
    }

    private async Task<Unit> UpdateFlow(BuyingStoreSaveCommand request, BuyingStore storeInDb)
    {
        var storeId = storeInDb.Id;
        _mapper.Map(request, storeInDb);
        storeInDb.Id = storeId;

        await _storeRepository.Update(storeInDb);

        _ = await _storeItemRepository.DeleteAll(storeInDb.Id);

        await _storeItemRepository.Save(storeInDb.BuyingStoreItem);

        return new Unit();
    }

    private Task Publish(NewStoreNotification newStoreNotification)
        => _mediator.Publish(newStoreNotification);

}
