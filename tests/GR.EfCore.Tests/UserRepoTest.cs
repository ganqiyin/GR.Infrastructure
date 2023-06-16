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
        public async Task GetByIdTest()
        {
            var user = await _userRepo.GetAsync(1);
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }


        [Fact]
        public async Task GetAllTest()
        {
            var users = await _userRepo.GetAllAsync();
            Assert.NotNull(users);
        }
    }
}
