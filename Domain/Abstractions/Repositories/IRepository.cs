using System;
using System.Linq.Expressions;

namespace Domain.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        void Remove(int id);
        TEntity Update(TEntity entity);
    }
}
