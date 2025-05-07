namespace Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;

using FunctionalConcepts;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;
using CharacterId = int;
using Item = string;
using StoreId = int;

public interface IBuyingStoreItemRepository : IStoreRepository<BuyingStoreItem>
{
    Task<Success> DeleteAll(CharacterId id);
    IQueryable<BuyingStoreItem> GetAllByCharacterId(StoreId storeId);
    IQueryable<BuyingStoreItem> GetAllByItemName(Item name);
}
