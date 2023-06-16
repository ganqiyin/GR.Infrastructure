using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GR.EfCore.Tests.Repository
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddEntityTypeConfigurations<TestDbContext>();
        }
    }
}
