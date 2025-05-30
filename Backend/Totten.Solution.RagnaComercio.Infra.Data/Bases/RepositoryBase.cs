﻿namespace Totten.Solution.RagnaComercio.Infra.Data.Bases;

using FunctionalConcepts;
using FunctionalConcepts.Options;
using FunctionalConcepts.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.Domain.Bases;

public abstract class RepositoryBase<TEntity, TId>(DbContext context)
    : IRepository<TEntity, TId>
    where TEntity : notnull, Entity<TEntity, TId>
    where TId : notnull
{
    protected readonly DbContext _context = context;

    public IQueryable<TEntity> GetAll()
        => _context.Set<TEntity>()
        .AsNoTracking();

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        => _context
        .Set<TEntity>()
        .Where(filter)
        .AsNoTracking();

    public Option<TEntity> Get(Expression<Func<TEntity, bool>> filter)
    {
        var entity = _context
        .Set<TEntity>()
        .AsNoTracking()
        .Where(filter)
        .FirstOrDefault();

        return entity is null ? NoneType.Value : entity;
    }

    public async Task<Option<TEntity>> GetById(TId id)
    {
        var entity = await _context
       .Set<TEntity>()
       .FindAsync(id);

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
