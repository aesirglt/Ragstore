namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.BuyingStores;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public class BuyingStoreRepository(ServerStoreContext context)
    : RepositoryBase<BuyingStore>(context), IBuyingStoreRepository
{
    public IQueryable<BuyingStore> GetAll()
        => _context
            .Set<BuyingStore>()
            .Include(x => x.BuyingStoreItem)
            .Include(x => x.Character)
            .AsNoTracking();

    public BuyingStore? GetByCharacterId(int id)
    {
        return _context
            .Set<BuyingStore>()
            .Include(x => x.BuyingStoreItem)
            .AsNoTracking()
            .FirstOrDefault(x => x.CharacterId == id);
    }
}
