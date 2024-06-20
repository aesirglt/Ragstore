namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class ItemCollectionByNameQueryHandler(IItemRepository storeRepository) : IRequestHandler<ItemCollectionByNameQuery, Result<IQueryable<Item>>>
{
    private readonly IItemRepository _storeRepository = storeRepository;

    public async Task<Result<IQueryable<Item>>> Handle(ItemCollectionByNameQuery request, CancellationToken cancellationToken)
    {
        var returned = Result.Of(_storeRepository.GetAllByName(request.Name));

        return await returned.AsTask();
    }
}
