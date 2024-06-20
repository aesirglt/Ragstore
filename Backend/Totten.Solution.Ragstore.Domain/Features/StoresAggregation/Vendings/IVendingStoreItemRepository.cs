namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

using FunctionalConcepts;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
using CharacterId = int;
using Item = string;
using StoreId = int;

public interface IVendingStoreItemRepository : IStoreRepository<VendingStoreItem>
{
    Task<Success> DeleteAll(CharacterId id);
    IQueryable<VendingStoreItem> GetAllByCharacterId(StoreId storeId);
    IQueryable<VendingStoreItem> GetAllByItemName(Item name);
}
