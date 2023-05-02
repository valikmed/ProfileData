using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions.Repositories;

namespace Application.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }


        public TEntity Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Save();
            return entity;
        }

        public void Remove(int id)
        {
            if (Context.Set<TEntity>() != null)
            {
                Context.Set<TEntity>().Remove(Context.Set<TEntity>().Find(id));
                Save();
            }
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Save();
            return entity;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
