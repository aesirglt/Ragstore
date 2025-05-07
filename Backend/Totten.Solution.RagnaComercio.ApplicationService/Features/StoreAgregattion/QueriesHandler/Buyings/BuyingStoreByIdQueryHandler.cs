namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.QueriesHandler.Buyings;

using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;

public class BuyingStoreByIdQueryHandler(IBuyingStoreRepository storeRepository) : IRequestHandler<BuyingStoreByIdQuery, Result<BuyingStore>>
{
    private readonly IBuyingStoreRepository _storeRepository = storeRepository;

    public async Task<Result<BuyingStore>> Handle(BuyingStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetById(request.Id);

        return store.Match(some => Result.Of(some), () => (NotFoundError)"");
    }
}
