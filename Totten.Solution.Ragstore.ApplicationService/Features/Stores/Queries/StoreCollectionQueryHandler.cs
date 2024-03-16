namespace Totten.Solution.Ragstore.ApplicationService.Features.Stores.Queries;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.QueriesCommand;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreCollectionQueryHandler : IRequestHandler<StoreCollectionQuery, Result<Exception, List<Store>>>
{
    private IStoreRepository _storeRepository;

    public StoreCollectionQueryHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, List<Store>>> Handle(StoreCollectionQuery request, CancellationToken cancellationToken)
    {
        return await _storeRepository.GetAll();
    }
}
