namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.BuyingStores;

using FunctionalConcepts.Options;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public class BuyingStoreRepository(ServerStoreContext context)
    : RepositoryBase<BuyingStore>(context), IBuyingStoreRepository
{
    public Option<BuyingStore> GetByCharacterId(int id)
    {
        var entity = _context
            .Set<BuyingStore>()
            .AsNoTracking()
            .FirstOrDefault(x => x.CharacterId == id);

        return entity is null ? NoneType.Value : entity;
    }
}
