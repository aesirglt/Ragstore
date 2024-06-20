namespace Totten.Solution.Ragstore.Infra.Data.Bases;

using FunctionalConcepts;
using FunctionalConcepts.Options;
using FunctionalConcepts.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Bases;

public abstract class RepositoryBase<TEntity>(DbContext context)
    : IRepository<TEntity, int>
    where TEntity : notnull, Entity<TEntity, int>
{
    protected readonly DbContext _context = context;

    public IQueryable<TEntity> GetAll()
        => _context.Set<TEntity>().AsNoTracking();

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        => _context
        .Set<TEntity>()
        .Where(filter)
        .AsNoTracking();

    public Option<TEntity?> Get(Expression<Func<TEntity, bool>> filter)
        => _context
        .Set<TEntity>()
        .Where(filter)
        .AsNoTracking()
        .FirstOrDefault();

    public async Task<Option<TEntity>> GetById(int id)
    {
        var query = _context
       .Set<TEntity>()
       .AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Success> Remove(TEntity entity)
    {
        _context
           .Set<TEntity>()
           .Entry(entity).State = EntityState.Deleted;

        await _context.SaveChangesAsync();
        return Result.Success;
    }

    public async Task<Success> Save(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return default;
    }

    public async Task<Success> Update(TEntity entity)
    {
        _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return default;
    }
}
