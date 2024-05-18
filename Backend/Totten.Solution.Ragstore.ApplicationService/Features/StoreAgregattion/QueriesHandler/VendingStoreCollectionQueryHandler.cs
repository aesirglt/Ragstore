namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class VendingStoreCollectionQueryHandler : IRequestHandler<VendingStoreCollectionQuery, Result<Exception, IQueryable<VendingStore>>>
{
    private IVendingStoreRepository _storeRepository;

    public VendingStoreCollectionQueryHandler(
        IVendingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, IQueryable<VendingStore>>> Handle(VendingStoreCollectionQuery request, CancellationToken cancellationToken)
    {
        var result = Result<Exception, IQueryable<VendingStore>>.Ok(_storeRepository.GetAll());

        return await Task.Run(() => result);
    }
}
