using Microsoft.EntityFrameworkCore;

namespace OneAspNet.Repository.EntityFramework
{
    public class OneAspNetDbContext : DbContext
    {
        public OneAspNetDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
