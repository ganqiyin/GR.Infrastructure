using GR.EfCore.Tests.Repository;
using GR.EfCore.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace GR.EfCore.Tests
{
    public class UnitOfWorkTest : IClassFixture<EfCoreFixture>
    {
        private readonly IUserRepo _userRepo;
        private readonly IProductRepo _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkTest(EfCoreFixture fixture)
        {
            _userRepo = fixture._Host.Services.GetRequiredService<IUserRepo>();
            _unitOfWork = fixture._Host.Services.GetRequiredService<IUnitOfWork>();
            _productRepo = fixture._Host.Services.GetRequiredService<IProductRepo>();
        }

        [Fact]
        public async Task AddUserAndProductTest()
        {
            await _userRepo.InsertAsync(new Domain.User { Name = "test", Password = "123456" });
            await _productRepo.InsertAsync(new Domain.Product { Name = "test", Code = "123456" });
            var count = await _unitOfWork.CommitAsync();
            Assert.True(count > 0);
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task AddUserTest()
        {
            await _userRepo.InsertAsync(new Domain.User { Name = "test", Password = "123456" });
            var count = await _unitOfWork.CommitAsync();
            Assert.True(count > 0);
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task AddProductTest()
        {
            await _productRepo.InsertAsync(new Domain.Product { Name = "test", Code = "123456" });
            var count = await _unitOfWork.CommitAsync();
            Assert.True(count > 0);
            Assert.Equal(1, count);
        }
    }
}
