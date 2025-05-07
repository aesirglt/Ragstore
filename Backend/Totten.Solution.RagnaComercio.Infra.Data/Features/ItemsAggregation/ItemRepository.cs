namespace Totten.Solution.RagnaComercio.Infra.Data.Features.ItemAggregation;

using Microsoft.EntityFrameworkCore;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;

public class ItemRepository(ServerStoreContext context)
    : RepositoryBase<Item, int>(context), IItemRepository
{
    public IQueryable<Item> GetAllByName(string name)
    {
        return _context
            .Set<Item>()
            .AsNoTracking()
            .Where(t => t.Name.Contains(name));
    }
}
