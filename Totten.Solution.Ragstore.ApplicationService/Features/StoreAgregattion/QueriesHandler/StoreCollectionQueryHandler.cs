namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreCollectionQueryHandler : IRequestHandler<StoreCollectionQuery, Result<Exception, List<VendingStore>>>
{
    private IVendingStoreItemRepository _vendingStoreItemRepository;
    private IVendingStoreRepository _storeRepository;

    public StoreCollectionQueryHandler(
        IVendingStoreRepository storeRepository,
        IVendingStoreItemRepository vendingStoreItemRepository)
    {
        _storeRepository = storeRepository;
        _vendingStoreItemRepository = vendingStoreItemRepository;
    }

    public async Task<Result<Exception, List<VendingStore>>> Handle(StoreCollectionQuery request, CancellationToken cancellationToken)
    {
        return ( await _storeRepository.GetAll() )
               .Select(vending =>
               {
                   vending.VendingStoreItems = _vendingStoreItemRepository.GetAllByCharacterId(vending.CharacterId);
                   return vending;
               }).ToList();
    }
}
