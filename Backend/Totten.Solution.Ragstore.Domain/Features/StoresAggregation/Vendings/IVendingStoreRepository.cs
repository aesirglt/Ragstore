namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

using FunctionalConcepts.Options;
using Totten.Solution.Ragstore.Domain.Bases;

public interface IVendingStoreRepository : IRepository<VendingStore, int>
{
    Option<VendingStore> GetByCharacterId(int id);
}
