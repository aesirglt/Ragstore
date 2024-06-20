namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class VendingStoreByIdQueryHandler(IVendingStoreRepository storeRepository) : IRequestHandler<VendingStoreByIdQuery, Result<VendingStore>>
{
    private readonly IVendingStoreRepository _storeRepository = storeRepository;

    public async Task<Result<VendingStore>> Handle(VendingStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var maybeStore = await _storeRepository.GetById(request.Id);
        return maybeStore.Match(store => Result.Of(store),
                                () => (NotFoundError)("Vending not found"));
    }
}
