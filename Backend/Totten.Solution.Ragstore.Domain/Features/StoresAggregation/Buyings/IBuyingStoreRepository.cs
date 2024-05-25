namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;

using LanguageExt;
using Totten.Solution.Ragstore.Domain.Bases;

public interface IBuyingStoreRepository : IRepository<BuyingStore, int>
{
    Option<BuyingStore> GetByCharacterId(int id);
}
