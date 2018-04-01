using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OneAspNet.Repository.EntityFramework
{
    public partial class EfRepository<T> : IEfRepository<T> where T : class, new()
    {
        public OneAspNetDbContext _dbContext { get; set; }
        public EfRepository(OneAspNetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }
        public T[] AddRange(T[] entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            return entities;
        }

        public T Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return entity;
        }
        public T[] RemoveRange(T[] entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return entities;
        }

        public T Update(T entity, List<string> specifyProperties = null)
        {
            _dbContext.Set<T>().Attach(entity);
            if (specifyProperties != null && specifyProperties.Count > 0)
            {
                foreach (var item in specifyProperties)
                {
                    _dbContext.Entry(entity).Property(item).IsModified = true;
                }
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            return entity;
        }

        public T[] UpdateRange(T[] entities, List<string> specifyProperties = null)
        {
            _dbContext.Set<T>().AttachRange(entities);
            foreach (var entity in entities)
            {
                if (specifyProperties != null && specifyProperties.Count > 0)
                {
                    foreach (var property in specifyProperties)
                    {
                        _dbContext.Entry(entity).Property(property).IsModified = true;
                    }
                }
                else
                {
                    _dbContext.Entry(entity).State = EntityState.Modified;
                }
            }
            return entities;
        }

        public T Find(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> whereLambda)
        {
            return _dbContext.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> Find<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderLambda, bool isAsc)
        {
            var temp = _dbContext.Set<T>().Where(whereLambda);
            totalCount = temp.Count();
            if (isAsc)//升序
            {
                temp = temp.OrderBy<T, S>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, S>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
