namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;

public class BuyingStoreCollectionQueryHandler : IRequestHandler<BuyingStoreCollectionQuery, Result<IQueryable<BuyingStore>>>
{
    private IBuyingStoreRepository _storeRepository;

    public BuyingStoreCollectionQueryHandler(
        IBuyingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<IQueryable<BuyingStore>>> Handle(BuyingStoreCollectionQuery request, CancellationToken cancellationToken)
    {
        var stores = await _storeRepository.GetAll().AsTask();

        return new Result<IQueryable<BuyingStore>>(stores);
    }
}
