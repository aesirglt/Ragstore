namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesCommand;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreCollectionQueryHandler : IRequestHandler<StoreCollectionQuery, Result<Exception, List<VendingStore>>>
{
    private IVendingStoreRepository _storeRepository;

    public StoreCollectionQueryHandler(IVendingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, List<VendingStore>>> Handle(StoreCollectionQuery request, CancellationToken cancellationToken)
    {
        return await _storeRepository.GetAll();
    }
}
