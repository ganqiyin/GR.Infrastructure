using GR.EfCore.Tests.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GR.EfCore.Tests
{
    public class UserRepoTest : IClassFixture<EfCoreFixture>
    {
        private readonly IUserRepo _userRepo;
        public UserRepoTest(EfCoreFixture fixture)
        {
            _userRepo = fixture._Host.Services.GetRequiredService<IUserRepo>();
        }

        [Fact]
        public async Task AddTest()
        {
            var count = await _userRepo.InsertAsync(new Domain.User { Id = 1, Name = "test", Password = "123456" });
            Assert.True(count > 0);
            Assert.Equal(1, count);
        }
    }
}
