namespace Totten.Solution.Ragstore.ApplicationService.Features.Stores.Handlers;

using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.HandlersCommand;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stories;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class StoreSaveCommandHandler : IRequestHandler<StoreSaveCommand, Result<Exception, Unit>>
{
    private IMediator _mediator;
    private IMapper _mapper;
    private IStoreRepository _storeRepository;

    public StoreSaveCommandHandler(IMediator mediator, IMapper mapper, IStoreRepository storeRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, Unit>> Handle(StoreSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var store = _mapper.Map<Store>(request);

            _ = await _storeRepository.Save(store);

            _ = _mediator.Publish(new NewStoreNotification
            {
                Mercant = request.SellerName,
                Location = request.Location,
                Date = request.CreationDate,
                Items = request.Items
            });

            return new Unit();
        }
        catch (Exception ex)
        {
            return new InternalError("Erro ao salvar uma nova loja", ex);
        }
    }
}
