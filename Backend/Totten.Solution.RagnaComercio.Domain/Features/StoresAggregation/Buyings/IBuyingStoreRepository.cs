namespace Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;

using FunctionalConcepts.Options;
using Totten.Solution.RagnaComercio.Domain.Bases;

public interface IBuyingStoreRepository : IRepository<BuyingStore, int>
{
    Option<BuyingStore> GetByCharacterId(int id);
}
