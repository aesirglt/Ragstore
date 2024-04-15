namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using CharacterId = int;
using StoreId = int;

public interface IVendingStoreItemRepository : IRepository<VendingStoreItem, int>
{
    Task<Unit> DeleteAll(CharacterId id);
    List<VendingStoreItem> GetAllByCharacterId(StoreId storeId);
}
