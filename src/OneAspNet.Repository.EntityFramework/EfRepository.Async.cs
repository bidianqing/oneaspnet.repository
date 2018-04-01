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

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
