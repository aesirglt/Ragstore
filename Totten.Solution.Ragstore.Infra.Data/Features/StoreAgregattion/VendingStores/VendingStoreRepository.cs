namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion.VendingStores;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public class VendingStoreRepository : IVendingStoreRepository
{
    private readonly RagnaStoreContext _context;
    public VendingStoreRepository(RagnaStoreContext context)
    {
        _context = context;
    }

    public Task<List<VendingStore>> GetAll()
    {
        return _context.VendingStores.AsNoTracking().ToListAsync();
    }

    public async Task<VendingStore> GetById(int id)
        => await _context.VendingStores.FindAsync(id);

    public Task<List<VendingStore>> GetAllByFilter(Expression<Func<VendingStore, bool>> filter)
        => _context.VendingStores.Where(filter).AsNoTracking().ToListAsync();

    public async Task<Unit> Save(VendingStore store)
    {
        try
        {
            _context.VendingStores.Add(store);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new Unit();
        }
        return new Unit();
    }

    public async Task<Unit> SaveBatch(IQueryable<VendingStore> stores)
    {
        _context.VendingStores.AddRange(stores);
        await _context.SaveChangesAsync();
        return new Unit();
    }

    public async Task<Unit> Update(VendingStore entity)
    {
         _context.VendingStores.Update(entity);
        await _context.SaveChangesAsync();
        return new Unit();
    }

    public async Task<Unit> Remove(VendingStore entity)
    {
        _context.VendingStores.Remove(entity);
        await _context.SaveChangesAsync();
        return new Unit();
    }
}
