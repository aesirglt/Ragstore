namespace Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class ItemCollectionQueryHandler(IItemRepository storeRepository) : IRequestHandler<ItemCollectionQuery, Result<IQueryable<Item>>>
{
    private readonly IItemRepository _storeRepository = storeRepository;

    public async Task<Result<IQueryable<Item>>> Handle(ItemCollectionQuery request, CancellationToken cancellationToken)
    {
        var returned = Result.Of(_storeRepository.GetAll());

        return await returned.AsTask();
    }
}
