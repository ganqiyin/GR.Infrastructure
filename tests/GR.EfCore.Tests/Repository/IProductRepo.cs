using GR.Autofac;
using GR.EfCore.Repository;
using GR.EfCore.Tests.Domain;

namespace GR.EfCore.Tests.Repository
{
    public interface IProductRepo : IEfCoreRepository<TestDbContext, Product>, IScopedDenpency
    {
    }
}
