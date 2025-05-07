namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler.Buyings;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class BuyingStoreCollectionQueryHandler(
    IBuyingStoreRepository storeRepository) : IRequestHandler<BuyingStoreCollectionQuery, Result<IQueryable<BuyingStore>>>
{
    private readonly IBuyingStoreRepository _storeRepository = storeRepository;

    public async Task<Result<IQueryable<BuyingStore>>> Handle(BuyingStoreCollectionQuery request, CancellationToken cancellationToken)
    {
        var stores = await _storeRepository.GetAll().AsTask();

        return Result.Of(stores);
    }
}
