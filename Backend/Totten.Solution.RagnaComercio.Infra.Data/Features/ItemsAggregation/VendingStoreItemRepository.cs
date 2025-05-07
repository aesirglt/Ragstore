namespace Totten.Solution.RagnaComercio.Infra.Data.Features.ItemsAggregation;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;
using ItemName = string;
using StoreId = int;

public class VendingStoreItemRepository(ServerStoreContext context)
    : RepositoryBase<VendingStoreItem, StoreId>(context), IVendingStoreItemRepository
{
    public async Task<Success> DeleteAll(StoreId id)
    {
        var stores = await _context
            .Set<VendingStoreItem>()
            .Where(x => x.StoreId == id)
            .AsNoTracking()
            .ToArrayAsync();

        _context.RemoveRange(stores);

        await _context.SaveChangesAsync();

        return Result.Success;
    }

    public IQueryable<VendingStoreItem> GetAllByCharacterId(int id)
        => _context
            .Set<VendingStoreItem>()
           .Where(item => item.CharacterId == id)
           .AsNoTracking();

    public IQueryable<VendingStoreItem> GetAllByItemName(ItemName name)
    => _context
           .Set<VendingStoreItem>()
           .Where(item => string.IsNullOrEmpty(name) || item.Name.Contains(name))
           .AsNoTracking();
}