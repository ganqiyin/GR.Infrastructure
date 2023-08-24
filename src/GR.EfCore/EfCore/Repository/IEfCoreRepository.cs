using GR.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GR.EfCore.Repository
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <remarks>
    /// 参考文档：
    /// https://cloud.tencent.com/developer/article/1505237?from=15425
    /// 
    /// </remarks>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IEfCoreRepository<TEntity, TDbContext> : IEfCoreRepository<TEntity, long, TDbContext>
        where TEntity : class, IEntity, IAggregateRoot
        where TDbContext : DbContext, new()
    { }

    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IEfCoreRepository<TEntity, TKey, TDbContext>
        where TEntity : class, IEntity<TKey>, IAggregateRoot<TKey>
        where TDbContext : DbContext, new()
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

        /// <summary>
        /// 查询
        /// </summary>
        IQueryable<TEntity> Query();

        /// <summary>
        /// 查询
        /// </summary>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询不跟踪实体变化
        /// </summary>
        IQueryable<TEntity> QueryNoTracking();

        /// <summary>
        /// 查询不跟踪实体变化
        /// </summary>
        IQueryable<TEntity> QueryNoTracking(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        TEntity Get(params object[] id);

        /// <summary>
        /// 根据主键获取(支持复合主键)
        /// </summary>
        ValueTask<TEntity> GetAsync(params object[] id);

        /// <summary>
        /// 根据主键(复合主键)
        /// </summary>
        ValueTask<TEntity> GetAsync(object[] ids, CancellationToken cancellationToken);

        /// <summary>
        /// 获取所有
        /// </summary>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// 获取所有
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式条件获取所有
        /// </summary>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据表达式条件获取所有
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式条件获取第一个或默认值
        /// </summary>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取第一个或默认值
        /// </summary>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 是否有数据
        /// </summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// 是否有数据
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 是否有数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 是否有数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 总数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    }
}
