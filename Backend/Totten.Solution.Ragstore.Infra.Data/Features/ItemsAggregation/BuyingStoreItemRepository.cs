namespace Totten.Solution.Ragstore.Infra.Data.Features.ItemsAggregation;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Item = string;
using StoreId = int;

public class BuyingStoreItemRepository(ServerStoreContext context)
    : RepositoryBase<BuyingStoreItem>(context), IBuyingStoreItemRepository
{
    public async Task<Unit> DeleteAll(StoreId id)
    {
        var stores = await _context
            .Set<BuyingStoreItem>()
            .Where(x => x.StoreId == id)
            .AsNoTracking()
            .ToArrayAsync();

        _context.RemoveRange(stores);

        await _context.SaveChangesAsync();

        return new Unit();
    }

    public IQueryable<BuyingStoreItem> GetAllByCharacterId(int id)
        => _context
            .Set<BuyingStoreItem>()
           .Where(item => item.CharacterId == id)
           .AsNoTracking();

    public IQueryable<BuyingStoreItem> GetAllByItemName(Item name)
        => _context
            .Set<BuyingStoreItem>()
           .Where(item => item.Name != null && item.Name.Contains(name))
           .AsNoTracking();
}