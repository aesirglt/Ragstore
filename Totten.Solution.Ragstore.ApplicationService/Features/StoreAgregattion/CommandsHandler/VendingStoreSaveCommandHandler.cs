namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.CommandsHandler;

using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stories;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class VendingStoreSaveCommandHandler : IRequestHandler<VendingStoreSaveCommand, Result<Exception, Unit>>
{
    private IMediator _mediator;
    private IMapper _mapper;
    private IVendingStoreRepository _storeRepository;

    public VendingStoreSaveCommandHandler(IMediator mediator, IMapper mapper, IVendingStoreRepository storeRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, Unit>> Handle(VendingStoreSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var store = _mapper.Map<VendingStore>(request);

            _ = await _storeRepository.Save(store);

            _ = _mediator.Publish(new NewStoreNotification
            {
                Server = "",
                Merchant = "",
                Location = "",
                Date = DateTime.Now,
                Items = new()
            });
            return new Unit();
        }
        catch (Exception ex)
        {
            return new InternalError("Erro ao salvar uma nova loja", ex);
        }
    }
}
