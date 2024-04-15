namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreByIdQueryHandler : IRequestHandler<StoreByIdQuery, Result<Exception, VendingStore>>
{
    private IVendingStoreRepository _storeRepository;

    public StoreByIdQueryHandler(IVendingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, VendingStore>> Handle(StoreByIdQuery request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetById(request.Id);

        if (store is null)
        {
            return new DirectoryNotFoundException();
        }



        return store;
    }
}
