namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using CharacterId = int;
using Item = string;
using StoreId = int;

public interface IVendingStoreItemRepository : IStoreRepository<VendingStoreItem>
{
    Task<Unit> DeleteAll(CharacterId id);
    IQueryable<VendingStoreItem> GetAllByCharacterId(StoreId storeId);
    IQueryable<VendingStoreItem> GetAllByItemName(Item name);
}
