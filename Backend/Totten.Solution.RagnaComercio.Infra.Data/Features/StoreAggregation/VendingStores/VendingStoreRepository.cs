namespace Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.VendingStores;

using FunctionalConcepts.Options;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;

public class VendingStoreRepository(ServerStoreContext context)
    : RepositoryBase<VendingStore, int>(context), IVendingStoreRepository
{
    public new IQueryable<VendingStore> GetAll()
        => _context
            .Set<VendingStore>()
            .Include(x => x.VendingStoreItems)
            .Include(x => x.Character)
            .AsNoTrackingWithIdentityResolution();

    public Option<VendingStore> GetByCharacterId(int id)
    {
        var entity = _context
            .Set<VendingStore>()
            .AsNoTracking()
            .FirstOrDefault(x => x.CharacterId == id);

        return entity is null
            ? NoneType.Value
            : entity;
    }
}
