namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class BuyingStoreByIdQueryHandler : IRequestHandler<BuyingStoreByIdQuery, Result<Exception, BuyingStore>>
{
    private IBuyingStoreRepository _storeRepository;

    public BuyingStoreByIdQueryHandler(IBuyingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, BuyingStore>> Handle(BuyingStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetById(request.Id);

        if (store is null) return new DirectoryNotFoundException();
        
        return store;
    }
}
