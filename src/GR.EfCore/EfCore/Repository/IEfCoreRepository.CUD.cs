using Microsoft.EntityFrameworkCore;

namespace GR.EfCore.Repository
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IEfCoreRepository<TDbContext, TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// 新增
        /// </summary>
        void Insert(TEntity entity);

        /// <summary>
        /// 新增
        /// </summary>
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量新增
        /// </summary>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 批量新增
        /// </summary>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 根据传入的实体删除
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// 根据传入的实体删除
        /// </summary>
        void Delete(IEnumerable<TEntity> entities);
    }
}
