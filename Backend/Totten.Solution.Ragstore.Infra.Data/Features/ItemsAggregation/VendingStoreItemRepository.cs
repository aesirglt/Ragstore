namespace Totten.Solution.Ragstore.Infra.Data.Features.ItemsAggregation;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;
using Item = string;
using StoreId = int;

public class VendingStoreItemRepository(RagnaStoreContext context)
    : RepositoryBase<VendingStoreItem>(context), IVendingStoreItemRepository
{
    public async Task<Unit> DeleteAll(StoreId id)
    {
        var stores = await _context
            .VendingStoreItems
            .Where(x => x.StoreId == id)
            .AsNoTracking()
            .ToArrayAsync();

        _context.RemoveRange(stores);

        await _context.SaveChangesAsync();

        return new Unit();
    }

    public IQueryable<VendingStoreItem> GetAllByCharacterId(int id)
        => _context
           .VendingStoreItems
           .Where(item => item.CharacterId == id)
           .AsNoTracking();

    public IQueryable<VendingStoreItem> GetAllByItemName(Item name)
        => _context
           .VendingStoreItems
           .Where(item => item.Name != null && item.Name.Contains(name))
           .AsNoTracking();
}