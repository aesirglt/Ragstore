namespace Totten.Solution.Ragstore.Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface IRepository<TEntity, TId>
    where TEntity : Entity<TEntity, TId>
    where TId : notnull
{
    Task<TEntity?> GetById(TId id);
    Task<List<TEntity>> GetAll();
    Task<List<TEntity>> GetAllByFilter(Expression<Func<TEntity, bool>> filter);
    Task<Unit> Save(TEntity entity);
    Task<Unit> Update(TEntity entity);
    Task<Unit> Remove(TEntity entity);
}
