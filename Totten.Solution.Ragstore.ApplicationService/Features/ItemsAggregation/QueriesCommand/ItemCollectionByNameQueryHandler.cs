namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.QueriesCommand;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ItemCollectionByNameQueryHandler : IRequestHandler<ItemCollectionByNameQuery, Result<Exception, List<Item>>>
{
    private IItemRepository _storeRepository;

    public ItemCollectionByNameQueryHandler(IItemRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Result<Exception, List<Item>>> Handle(ItemCollectionByNameQuery request, CancellationToken cancellationToken)
    {
        return new NotImplementedException();
    }
}
