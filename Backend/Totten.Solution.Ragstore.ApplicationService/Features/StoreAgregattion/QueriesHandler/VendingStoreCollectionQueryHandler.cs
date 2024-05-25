namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
public class VendingStoreCollectionQueryHandler : IRequestHandler<VendingStoreCollectionQuery, Result<IQueryable<VendingStore>>>
{
    private IVendingStoreRepository _storeRepository;

    public VendingStoreCollectionQueryHandler(
        IVendingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<IQueryable<VendingStore>>> Handle(VendingStoreCollectionQuery request, CancellationToken cancellationToken)
    {
        var result = await _storeRepository.GetAll().AsTask();

        return new Result<IQueryable<VendingStore>>(result);
    }
}
