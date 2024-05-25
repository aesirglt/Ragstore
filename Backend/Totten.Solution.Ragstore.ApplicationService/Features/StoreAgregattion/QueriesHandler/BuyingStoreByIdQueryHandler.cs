namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;

public class BuyingStoreByIdQueryHandler : IRequestHandler<BuyingStoreByIdQuery, Result<BuyingStore>>
{
    private IBuyingStoreRepository _storeRepository;

    public BuyingStoreByIdQueryHandler(IBuyingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<BuyingStore>> Handle(BuyingStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetById(request.Id);

        return store.Match(some => some, () => new Result<BuyingStore>(new NotFoundError("")));
    }
}
