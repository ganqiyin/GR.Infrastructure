using GR.EfCore.Repository.Impl;
using GR.EfCore.Tests.Domain;

namespace GR.EfCore.Tests.Repository
{
    public class ProductRepo : EfCoreRepositoryBase<TestDbContext, Product>, IProductRepo
    {
        public ProductRepo(TestDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
