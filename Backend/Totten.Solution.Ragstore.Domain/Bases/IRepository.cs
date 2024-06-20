namespace Totten.Solution.Ragstore.Domain.Bases;

using FunctionalConcepts;
using FunctionalConcepts.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IRepository<TEntity, TId>
    where TEntity : notnull, Entity<TEntity, TId>
    where TId : notnull
{
    Task<Option<TEntity>> GetById(TId id);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
    Option<TEntity> Get(Expression<Func<TEntity, bool>> filter);
    Task<Success> Save(TEntity entity);
    Task<Success> Update(TEntity entity);
    Task<Success> Remove(TEntity entity);
}
