namespace Totten.Solution.Ragstore.Infra.Data.Bases;

using FunctionalConcepts;
using FunctionalConcepts.Options;
using FunctionalConcepts.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
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
        => _context.Set<TEntity>()
        .AsNoTrackingWithIdentityResolution();

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        => _context
        .Set<TEntity>()
        .AsNoTrackingWithIdentityResolution()
        .Where(filter);

    public Option<TEntity> Get(Expression<Func<TEntity, bool>> filter)
    {
        var entity = _context
        .Set<TEntity>()
        .AsNoTrackingWithIdentityResolution()
        .Where(filter)
        .FirstOrDefault();

        return entity is null ? NoneType.Value : entity;
    }

    public async Task<Option<TEntity>> GetById(int id)
    {
        var query = _context
       .Set<TEntity>()
       .AsNoTrackingWithIdentityResolution();

        var entity = await query.FirstOrDefaultAsync(x => x.Id == id);

        return entity is null ? NoneType.Value : entity;
    }

    public async Task<Success> Remove(TEntity entity)
    {
        _context
           .Set<TEntity>()
           .Entry(entity).State = EntityState.Deleted;

        await _context.SaveChangesAsync();
        DetachEntityAndRelatedObjects(entity);
        return Result.Success;
    }

    public async Task<Success> Save(TEntity entity)
    {
        _ = _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();

        DetachEntityAndRelatedObjects(entity);

        return default;
    }

    public async Task<Success> Update(TEntity entity)
    {
        _ = _context.Set<TEntity>()
                    .Entry(entity)
                    .State = EntityState.Modified;
        await _context.SaveChangesAsync();
        DetachEntityAndRelatedObjects(entity);
        return default;
    }
    private void DetachEntityAndRelatedObjects(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Detached;

        var navigationProperties = _context.Entry(entity).Navigations;

        foreach (var navigation in navigationProperties)
        {
            if (navigation is CollectionEntry relatedCollection)
            {
                foreach (var relatedEntity in relatedCollection.Query())
                {
                    _context.Entry(relatedEntity).State = EntityState.Detached;
                }
            }
            else
            {
                var relatedEntity = navigation.CurrentValue;
                if (relatedEntity != null)
                {
                    _context.Entry(relatedEntity).State = EntityState.Detached;
                }
            }
        }
    }
}
