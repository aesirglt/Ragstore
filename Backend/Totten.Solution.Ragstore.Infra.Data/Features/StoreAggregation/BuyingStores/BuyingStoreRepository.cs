namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.BuyingStores;

using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public class BuyingStoreRepository(ServerStoreContext context)
    : RepositoryBase<BuyingStore>(context), IBuyingStoreRepository
{
    public IQueryable<BuyingStore> GetAllCompletedStores()
    {
        throw new NotImplementedException();
    }

    public BuyingStore? GetByCharacterId(int id)
    {
        throw new NotImplementedException();
    }
}
