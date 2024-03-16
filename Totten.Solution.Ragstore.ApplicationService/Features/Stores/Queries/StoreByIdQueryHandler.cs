namespace Totten.Solution.Ragstore.ApplicationService.Features.Stores.Queries;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.QueriesCommand;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreByIdQueryHandler : IRequestHandler<StoreByIdQuery, Result<Exception, Store>>
{
    private IStoreRepository _storeRepository;

    public StoreByIdQueryHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, Store>> Handle(StoreByIdQuery request, CancellationToken cancellationToken)
    {
        return await _storeRepository.GetById(request.Id);
    }
}
