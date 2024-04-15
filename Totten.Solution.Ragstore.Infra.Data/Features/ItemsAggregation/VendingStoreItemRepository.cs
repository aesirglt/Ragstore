namespace Totten.Solution.Ragstore.Infra.Data.Features.ItemsAggregation;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;
using StoreId = int;

public class VendingStoreItemRepository(RagnaStoreContext context)
    : RepositoryBase<VendingStoreItem>(context), IVendingStoreItemRepository
{
    public async Task<Unit> DeleteAll(StoreId id)
    {
        var stores = await _context
            .VendingStoreItems
            .Where(x => x.VendingStoreId == id)
            .AsNoTracking()
            .ToArrayAsync();

        _context.RemoveRange(stores);

        await _context.SaveChangesAsync();

        return new Unit();
    }

    public List<VendingStoreItem> GetAllByCharacterId(int id)
    {
        return _context
            .VendingStoreItems
            .AsNoTracking()
            .Where(vending => vending.CharacterId == id)
            .ToList();
    }
}