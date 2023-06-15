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
        int Insert(TEntity entity);

        /// <summary>
        /// 新增
        /// </summary>
        Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量新增
        /// </summary>
        int Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 批量新增
        /// </summary>
        Task<int> InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新
        /// </summary>
        int Update(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 根据传入的实体删除
        /// </summary>
        int Delete(TEntity entity);

        /// <summary>
        /// 根据传入的实体删除
        /// </summary>
        int Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 根据传入的实体删除
        /// </summary>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 根据传入的实体删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entities);
    }
}
