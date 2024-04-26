namespace Totten.Solution.Ragstore.Infra.Data.Features.ItemAggregation;

using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public class ItemRepository(ServerStoreContext context)
    : RepositoryBase<Item>(context), IItemRepository
{
    public IQueryable<Item> GetAllByName(string name)
    {
        return _context
            .Set<Item>()
            .AsNoTracking()
            .Where(t => t.Name.Contains(name));
    }
}
