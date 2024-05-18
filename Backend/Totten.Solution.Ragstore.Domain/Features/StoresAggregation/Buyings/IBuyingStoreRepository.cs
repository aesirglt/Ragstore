namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Bases;

public interface IBuyingStoreRepository : IRepository<BuyingStore, int>
{
    IQueryable<BuyingStore> GetAll();
    BuyingStore? GetByCharacterId(int id);
}
