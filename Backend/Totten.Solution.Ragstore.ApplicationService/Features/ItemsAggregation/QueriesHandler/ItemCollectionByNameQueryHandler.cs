namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

public class ItemCollectionByNameQueryHandler : IRequestHandler<ItemCollectionByNameQuery, Result<IQueryable<Item>>>
{
    private IItemRepository _storeRepository;

    public ItemCollectionByNameQueryHandler(IItemRepository storeRepository)
        => _storeRepository = storeRepository;

    public async Task<Result<IQueryable<Item>>> Handle(ItemCollectionByNameQuery request, CancellationToken cancellationToken)
    {
        var returned = new Result<IQueryable<Item>>(_storeRepository.GetAllByName(request.Name));

        return await returned.AsTask();
    }
}
