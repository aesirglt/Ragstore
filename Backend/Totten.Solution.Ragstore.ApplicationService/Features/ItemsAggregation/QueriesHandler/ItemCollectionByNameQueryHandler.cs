namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.QueriesHandler;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ItemCollectionByNameQueryHandler : IRequestHandler<ItemCollectionByNameQuery, Result<Exception, IQueryable<Item>>>
{
    private IItemRepository _storeRepository;

    public ItemCollectionByNameQueryHandler(IItemRepository storeRepository)
        => _storeRepository = storeRepository;

    public async Task<Result<Exception, IQueryable<Item>>> Handle(ItemCollectionByNameQuery request, CancellationToken cancellationToken)
    {
        var returned = _storeRepository.GetAllByName(request.Name);

        return await Result<Exception, IQueryable<Item>>.Ok(returned).AsTask();
    }
}
