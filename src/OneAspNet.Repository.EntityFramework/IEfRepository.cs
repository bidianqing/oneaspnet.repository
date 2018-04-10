using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OneAspNet.Repository.EntityFramework
{
    public partial interface IEfRepository<T> where T : class, new()
    {
        T Add(T entity);
        T[] AddRange(T[] entities);
        T Remove(T entity);
        T[] RemoveRange(T[] entities);
        T Update(T entity, List<string> specifyProperties = null);
        T[] UpdateRange(T[] entities, List<string> specifyProperties = null);
        T Find(int id);
        IQueryable<T> Find(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> Find<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderLambda, bool isAsc);
        IQueryable<T> FromSql(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, params object[] parameters);
        int SaveChanges();
    }
}
