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
         
        public virtual int Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }
         
        public virtual async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
         
        public virtual int Insert(IEnumerable<TEntity> entities)
        { 
            _dbContext.Set<TEntity>().AddRange(entities);
            return _dbContext.SaveChanges();
        }
         
        public virtual async Task<int> InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual int Update(TEntity entity)
        {  
            AttachIfNot(_dbContext, entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }

        public virtual int Update(IEnumerable<TEntity> entities)
        { 
            foreach (var entity in entities)
            {
                AttachIfNot(_dbContext, entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            return _dbContext.SaveChanges();
        } 

        public virtual async Task<int> UpdateAsync(TEntity entity)
        { 
            AttachIfNot(_dbContext, entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
         
        public virtual async Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        { 
            foreach (var entity in entities)
            {
                AttachIfNot(_dbContext, entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            return await _dbContext.SaveChangesAsync();
        }
         
        public virtual int Delete(TEntity entity)
        {
            AttachIfNot(_dbContext, entity);
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public virtual int Delete(IEnumerable<TEntity> entities)
        { 
            foreach (var entity in entities)
            {
                AttachIfNot(_dbContext, entity);
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            AttachIfNot(_dbContext, entity);
            _dbContext.Set<TEntity>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        { 
            foreach (var entity in entities)
            {
                AttachIfNot(_dbContext, entity);
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return await _dbContext.SaveChangesAsync();
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
