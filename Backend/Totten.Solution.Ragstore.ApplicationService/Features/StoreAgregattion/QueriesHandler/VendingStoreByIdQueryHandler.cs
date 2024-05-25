namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.QueriesHandler;

using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;

public class VendingStoreByIdQueryHandler : IRequestHandler<VendingStoreByIdQuery, Result<VendingStore>>
{
    private IVendingStoreRepository _storeRepository;

    public VendingStoreByIdQueryHandler(IVendingStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<VendingStore>> Handle(VendingStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var maybeStore = await _storeRepository.GetById(request.Id);
        return maybeStore.Match(store => new Result<VendingStore>(store),
                                () => new(new NotFoundError("")));
    }
}
