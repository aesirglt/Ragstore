namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.VendingStores;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public class VendingStoreRepository(RagnaStoreContext context)
    : RepositoryBase<VendingStore>(context), IVendingStoreRepository
{

}
