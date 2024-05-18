namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class VendingStoreByIdQueryHandler : IRequestHandler<VendingStoreByIdQuery, Result<Exception, VendingStore>>
{
    private IVendingStoreRepository _storeRepository;

    public VendingStoreByIdQueryHandler(IVendingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, VendingStore>> Handle(VendingStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetById(request.Id);

        if (store is null) return new DirectoryNotFoundException();
        
        return store;
    }
}
