namespace Totten.Solution.Ragstore.Domain.Bases;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface IRepository<TEntity, TId>
    where TEntity : notnull, Entity<TEntity, TId>
    where TId : notnull
{
    Task<TEntity?> GetById<TProperty>(TId id, params Expression<Func<TEntity, TProperty>>[] configure);
    IQueryable<TEntity> GetAllWith<TProperty>(params Expression<Func<TEntity, TProperty>>[] configure);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter);
    Task<Unit> Save(TEntity entity);
    Task<Unit> Update(TEntity entity);
    Task<Unit> Remove(TEntity entity);
}
