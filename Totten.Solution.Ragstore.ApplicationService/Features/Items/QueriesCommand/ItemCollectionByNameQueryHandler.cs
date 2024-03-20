namespace Totten.Solution.Ragstore.ApplicationService.Features.Items.QueriesCommand;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Items.Queries;
using Totten.Solution.Ragstore.Domain.Features.Items;
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
        return await _storeRepository.GetAllByDate(request.Name, DateTime.Now);
    }
}
