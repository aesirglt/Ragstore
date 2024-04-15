namespace Totten.Solution.Ragstore.Infra.Data.Bases;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity, int>
    where TEntity : Entity<TEntity, int>
{
    protected readonly RagnaStoreContext _context;

    public RepositoryBase(RagnaStoreContext context)
        => _context = context;

    public async Task<List<TEntity>> GetAll()
        => await _context
            .Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();

    public async Task<List<TEntity>> GetAllByFilter(Expression<Func<TEntity, bool>> filter)
        => await _context
        .Set<TEntity>()
        .Where(filter)
        .AsNoTracking()
        .ToListAsync();

    public async Task<TEntity?> GetById(int id)
        => await _context
        .Set<TEntity>()
        .FindAsync(id);

    public async Task<Unit> Remove(TEntity entity)
    {
         _context
            .Set<TEntity>()
            .Entry(entity).State = EntityState.Deleted;

        await _context.SaveChangesAsync();
        return new Unit();
    }

    public async Task<Unit> Save(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return new Unit();
    }

    public async Task<Unit> Update(TEntity entity)
    {
        _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return new Unit();
    }
}
