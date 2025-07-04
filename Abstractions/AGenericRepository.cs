using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BaseUtility
{
    public class AGenericRepository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
        where TContext : DbContext
    {
        protected readonly IDatabase<TContext> _db;
        protected readonly DbSet<TEntity> _dbSet;

        public AGenericRepository(IDatabase<TContext> db)
        {
            _db = db;
            _dbSet = _db.Context.Set<TEntity>();
        }

        private void EnsureNotTracked(TEntity entity)
        {
            var local = _dbSet.Local.FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(e.Id, entity.Id));
            if (local != null && !ReferenceEquals(local, entity))
            {
                // Detach the local instance if it's not the same as the incoming entity
                _db.Context.Entry(local).State = EntityState.Detached;
            }
        }

        public virtual async Task<RepositoryResponse<TEntity>> CreateAsync(TEntity entity)
        {
            if (entity is null)
            {
                return RepositoryResponse<TEntity>.EntityCannotBeNull();
            }
            try
            {
                EnsureNotTracked(entity);
                await _dbSet.AddAsync(entity);
                await _db.Context.SaveChangesAsync();
                return RepositoryResponse<TEntity>.EntityCreatedSuccessfully(entity);
            }
            catch
            {
                return RepositoryResponse<TEntity>.ErrorCreatingEntity();
            }
        }

        public virtual async Task<RepositoryResponse<TEntity>> DeleteAsync(TKey id)
        {
            if (id is null)
            {
                return RepositoryResponse<TEntity>.IdCannotBeNull();
            }
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity is null)
                {
                    return RepositoryResponse<TEntity>.EntityCannotBeNull();
                }
                _dbSet.Remove(entity);
                await _db.Context.SaveChangesAsync();
                return RepositoryResponse<TEntity>.EntityDeletedSuccessfully(entity);
            }
            catch
            {
                return RepositoryResponse<TEntity>.ErrorDeletingEntity();
            }
        }

        public virtual async Task<RepositoryResponse<IEnumerable<TEntity>>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate is null)
            {
                return RepositoryResponse<IEnumerable<TEntity>>.PredicateCannotBeNull();
            }
            try
            {
                var entities = _dbSet.Where(predicate).ToList();
                return RepositoryResponse<IEnumerable<TEntity>>.EntitiesRetrievedSuccessfully(entities);
            }
            catch
            {
                return RepositoryResponse<IEnumerable<TEntity>>.ErrorFindingEntities();
            }
        }

        public virtual async Task<RepositoryResponse<IEnumerable<TEntity>>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();
                return RepositoryResponse<IEnumerable<TEntity>>.EntitiesRetrievedSuccessfully(entities);
            }
            catch
            {
                return RepositoryResponse<IEnumerable<TEntity>>.ErrorRetrievingEntities();
            }
        }

        public virtual async Task<RepositoryResponse<TEntity>> GetByIdAsync(TKey id)
        {
            if (id is null)
            {
                return RepositoryResponse<TEntity>.IdCannotBeNull();
            }
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity is null)
                {
                    return RepositoryResponse<TEntity>.EntityNotFound();
                }
                return RepositoryResponse<TEntity>.EntityRetrievedSuccessfully(entity);
            }
            catch
            {
                return RepositoryResponse<TEntity>.ErrorRetrievingEntity();
            }
        }

        public virtual async Task<RepositoryResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            if (entity is null)
            {
                return RepositoryResponse<TEntity>.EntityCannotBeNull();
            }
            try
            {
                EnsureNotTracked(entity);
                _dbSet.Update(entity);
                await _db.Context.SaveChangesAsync();
                return RepositoryResponse<TEntity>.EntityUpdatedSuccessfully(entity);
            }
            catch
            {
                return RepositoryResponse<TEntity>.ErrorUpdatingEntity();
            }
        }
    }
}
