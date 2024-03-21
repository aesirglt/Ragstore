namespace Totten.Solution.Ragstore.ApplicationService.Features.Stores.Handlers;

using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.HandlersCommand;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stories;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class StoreSaveBatchCommandHandler : IRequestHandler<StoreSaveBatchCommand, Result<Exception, Unit>>
{
    private IMediator _mediator;
    private IMapper _mapper;
    private IStoreRepository _storeRepository;

    public StoreSaveBatchCommandHandler(IMediator mediator, IMapper mapper, IStoreRepository storeRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, Unit>> Handle(StoreSaveBatchCommand requests, CancellationToken cancellationToken)
    {
        try
        {
            var stores = _mapper.ProjectTo<VendingStore>(requests.SaveCommands.AsQueryable());

            _ = await _storeRepository.SaveBatch(stores);

            foreach (var request in requests.SaveCommands)
            {
                _ = _mediator.Publish(new NewStoreNotification
                {
                    Server = request.Server,
                    Merchant = request.SellerName,
                    Location = request.Location,
                    Date = request.CreationDate,
                    Items = request.Items
                });
            }

            return new Unit();
        }
        catch (Exception ex)
        {
            return new InternalError("Erro ao salvar uma nova loja", ex);
        }
    }
}
