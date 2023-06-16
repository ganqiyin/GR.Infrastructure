using GR.EfCore.Repository.Impl;
using GR.EfCore.Tests.Domain;

namespace GR.EfCore.Tests.Repository
{
    public class UserRepo : EfCoreRepositoryBase<TestDbContext, User>, IUserRepo
    {
        public UserRepo(TestDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
