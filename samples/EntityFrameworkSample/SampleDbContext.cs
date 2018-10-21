using EntityFrameworkSample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneAspNet.Repository.EntityFramework;

namespace EntityFrameworkSample
{
    public class SampleDbContext : OneAspNetDbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {

        }


        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tb_user");
        }

    }
}
