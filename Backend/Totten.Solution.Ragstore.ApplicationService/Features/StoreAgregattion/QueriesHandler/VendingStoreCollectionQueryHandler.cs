namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class VendingStoreCollectionQueryHandler(
    IVendingStoreRepository storeRepository) : IRequestHandler<VendingStoreCollectionQuery, Result<IQueryable<VendingStore>>>
{
    private readonly IVendingStoreRepository _storeRepository = storeRepository;

    public async Task<Result<IQueryable<VendingStore>>> Handle(VendingStoreCollectionQuery request, CancellationToken cancellationToken)
    {
        var result = await _storeRepository.GetAll().AsTask();

        return Result.Of(result);
    }
}
