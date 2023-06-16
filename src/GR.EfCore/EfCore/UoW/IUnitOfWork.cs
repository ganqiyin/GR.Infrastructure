using Microsoft.EntityFrameworkCore;

namespace GR.EfCore.UoW
{
    public interface IUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        TDbContext DbContext { get; }
    }

    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        int Commit();
    }
}
