using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GR.EfCore.Repository.Impl
{
    public partial class EfCoreRepositoryBase<TDbContext, TEntity>
    {
        public IQueryable<TEntity> Query() => Table.AsQueryable();

        public IQueryable<TEntity> QueryNoTracking() => Table.AsNoTracking();

        public virtual TEntity Get(params object[] id) => Table.Find(id);

        public virtual ValueTask<TEntity> GetAsync(params object[] id)
            => Table.FindAsync(id);

        public virtual ValueTask<TEntity> GetAsync(object[] ids, CancellationToken cancellationToken = default)
            => Table.FindAsync(ids, cancellationToken);

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate) => Query().Where(predicate);

        public virtual IQueryable<TEntity> QueryNoTracking(Expression<Func<TEntity, bool>> predicate) => QueryNoTracking().Where(predicate);

        public virtual IEnumerable<TEntity> GetAll() => Query().ToList();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) => await Query().ToListAsync(cancellationToken);

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate) => Query().Where(predicate).ToList();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => await Query().Where(predicate).ToListAsync(cancellationToken);

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => Query().FirstOrDefault(predicate);

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => Query().FirstOrDefaultAsync(predicate, cancellationToken);

        public virtual bool Any() => Query().Any();

        public virtual Task<bool> AnyAsync(CancellationToken cancellationToken = default) => Query().AnyAsync(cancellationToken);

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate) => Query().Any(predicate);

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => Query().AnyAsync(predicate, cancellationToken);

        public virtual int Count() => Query().Count();

        public virtual Task<int> CountAsync(CancellationToken cancellationToken = default) => Query().CountAsync(cancellationToken);

        public virtual int Count(Expression<Func<TEntity, bool>> predicate) => Query().Where(predicate).Count();

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => Query().CountAsync(predicate, cancellationToken);

        protected DbSet<TEntity> Table => _dbContext.Set<TEntity>();
    }
}
