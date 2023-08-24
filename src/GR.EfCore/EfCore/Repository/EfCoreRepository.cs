using GR.EfCore.UoW;
using GR.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GR.EfCore.Repository
{

    public class EfCoreRepository<TEntity, TDbContext> : EfCoreRepository<TEntity, long, TDbContext>, IEfCoreRepository<TEntity, TDbContext>
      where TEntity : class, IEntity, IAggregateRoot
      where TDbContext : DbContext, new()
    {
        public EfCoreRepository(TDbContext dbContext, IUnitOfWork unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }
    }

    public class EfCoreRepository<TEntity, TKey, TDbContext> : IEfCoreRepository<TEntity, TKey, TDbContext>
        where TEntity : class, IEntity<TKey>, IAggregateRoot<TKey>
        where TDbContext : DbContext, new()
    {
        private readonly TDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public EfCoreRepository(TDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public virtual void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken: cancellationToken);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken: cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            AttachIfNot(_dbContext, entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                AttachIfNot(_dbContext, entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }


        public virtual void Delete(TEntity entity)
        {
            AttachIfNot(_dbContext, entity);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                AttachIfNot(_dbContext, entity);
                _dbContext.Set<TEntity>().Remove(entity);
            }
        }

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

        protected virtual void AttachIfNot(DbContext dbContext, TEntity entity)
        {
            var entry = dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }
            dbContext.Set<TEntity>().Attach(entity);
        }
    }
}
