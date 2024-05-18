namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class BuyingStoreCollectionQueryHandler : IRequestHandler<BuyingStoreCollectionQuery, Result<Exception, IQueryable<BuyingStore>>>
{
    private IBuyingStoreRepository _storeRepository;

    public BuyingStoreCollectionQueryHandler(
        IBuyingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, IQueryable<BuyingStore>>> Handle(BuyingStoreCollectionQuery request, CancellationToken cancellationToken)
    {
        var result = Result<Exception, IQueryable<BuyingStore>>.Ok(_storeRepository.GetAll());

        return await Task.Run(() => result);
    }
}
