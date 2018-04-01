using OneAspNet.Repository.EntityFramework;

namespace EntityFrameworkSample
{
    public class SampleRepository<T> : EfRepository<T> where T : class, new()
    {
        public SampleRepository(SampleDbContext dbContext) : base(dbContext)
        {

        }
    }
}
