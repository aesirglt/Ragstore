namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

using LanguageExt;
using Totten.Solution.Ragstore.Domain.Bases;

public interface IVendingStoreRepository : IRepository<VendingStore, int>
{
    Option<VendingStore> GetByCharacterId(int id);
}
