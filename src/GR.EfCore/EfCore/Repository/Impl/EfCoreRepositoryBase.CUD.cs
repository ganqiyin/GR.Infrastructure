using Microsoft.EntityFrameworkCore;

namespace GR.EfCore.Repository.Impl
{
    public partial class EfCoreRepositoryBase<TDbContext, TEntity> : IEfCoreRepository<TDbContext, TEntity>
         where TEntity : class
        where TDbContext : DbContext
    {

        private readonly TDbContext _dbContext;

        public EfCoreRepositoryBase(TDbContext dbContext)
        {
            _dbContext = dbContext;
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
