using EntityFrameworkSample.Domain;
using Microsoft.EntityFrameworkCore;
using OneAspNet.Repository.EntityFramework;

namespace EntityFrameworkSample
{
    public class SampleDbContext : OneAspNetDbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {

        }


        public virtual DbSet<User> Users { get; set; }

    }
}
