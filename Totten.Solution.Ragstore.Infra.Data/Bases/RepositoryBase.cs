namespace Totten.Solution.Ragstore.Infra.Data.Bases;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity, int>
    where TEntity : notnull, Entity<TEntity, int>
{
    protected readonly RagnaStoreContext _context;

    public RepositoryBase(RagnaStoreContext context)
        => _context = context;

    public IQueryable<TEntity> GetAll<TProperty>(params Expression<Func<TEntity, TProperty>>[] configure)
    {
        var query = _context
        .Set<TEntity>()
        .AsNoTracking();

        foreach (var cfg in configure ?? [])
        {
            query = query.Include(cfg);
        }

        return query;
    }
    public IQueryable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter)
        => _context
        .Set<TEntity>()
        .Where(filter)
        .AsNoTracking();

    public async Task<TEntity?> GetById<TProperty>(int id, params Expression<Func<TEntity, TProperty>>[] configure)
    {
        var query = _context
       .Set<TEntity>()
       .AsNoTracking();

        foreach (var cfg in configure ?? [])
        {
            query = query.Include(cfg);
        }

        return await query.FirstAsync(x => x.Id == id);
    }

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
