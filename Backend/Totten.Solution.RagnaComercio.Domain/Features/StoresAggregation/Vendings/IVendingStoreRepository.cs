namespace Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

using FunctionalConcepts.Options;
using Totten.Solution.RagnaComercio.Domain.Bases;

public interface IVendingStoreRepository : IRepository<VendingStore, int>
{
    Option<VendingStore> GetByCharacterId(int id);
}
