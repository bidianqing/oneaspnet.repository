using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneAspNet.Repository.EntityFramework
{
    public partial interface IEfRepository<T> where T : class, new()
    {
        Task<T> AddAsync(T entity);
        Task<T[]> AddRangeAsync(T[] entities);
        Task<T> FindAsync(int id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereLambda);
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        Task<int> SaveChangesAsync();
    }
}
