using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneAspNet.Repository.EntityFramework
{
    public partial class EfRepository<T> where T : class, new()
    {
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T[]> AddRangeAsync(T[] entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(whereLambda);
        }

        public async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
