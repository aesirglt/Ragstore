namespace Totten.Solution.Ragstore.Infra.Data.Features.Items;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public class ItemRepository : IItemRepository
{
    private readonly RagnaStoreContext _context;

    public ItemRepository(RagnaStoreContext context)
    {
        _context = context;
    }

    public Task<List<Item>> GetAll()
    {
        return _context.Items.AsNoTracking().ToListAsync();
    }

    public Task<List<Item>> GetAllByFilter(Expression<Func<Item, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task<Item> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Unit> Remove(Item entity)
    {
        throw new NotImplementedException();
    }

    public Task<Unit> Save(Item entity)
    {
        throw new NotImplementedException();
    }

    public Task<Unit> Update(Item entity)
    {
        throw new NotImplementedException();
    }
}
