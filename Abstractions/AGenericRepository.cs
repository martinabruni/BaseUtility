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
        protected readonly ResponseMessageOption _messages;

        public AGenericRepository(IDatabase<TContext> db, ResponseMessageOption messages)
        {
            _db = db;
            _dbSet = _db.Context.Set<TEntity>();
            _messages = messages;
        }

        private void EnsureNotTracked(TEntity entity)
        {
            var local = _dbSet.Local.FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(e.Id, entity.Id));
            if (local != null && !ReferenceEquals(local, entity))
            {
                _db.Context.Entry(local).State = EntityState.Detached;
            }
        }

        public virtual async Task<RepositoryResponse<TEntity>> CreateAsync(TEntity entity)
        {
            if (entity is null)
            {
                return RepositoryResponse<TEntity>.BadRequest(_messages.InvalidRequest);
            }
            try
            {
                EnsureNotTracked(entity);
                await _dbSet.AddAsync(entity);
                await _db.Context.SaveChangesAsync();
                return RepositoryResponse<TEntity>.Created(_messages.EntityCreatedSuccessfully, entity);
            }
            catch
            {
                return RepositoryResponse<TEntity>.InternalServerError(_messages.ErrorCreatingEntity);
            }
        }

        public virtual async Task<RepositoryResponse<TEntity>> DeleteAsync(TKey id)
        {
            if (id is null)
            {
                return RepositoryResponse<TEntity>.BadRequest(_messages.InvalidRequest);
            }
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity is null)
                {
                    return RepositoryResponse<TEntity>.NotFound(_messages.InvalidRequest);
                }
                _dbSet.Remove(entity);
                await _db.Context.SaveChangesAsync();
                return RepositoryResponse<TEntity>.Ok(_messages.EntityDeletedSuccessfully, entity);
            }
            catch
            {
                return RepositoryResponse<TEntity>.InternalServerError(_messages.ErrorDeletingEntity);
            }
        }

        public virtual Task<RepositoryResponse<IEnumerable<TEntity>>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate is null)
            {
                return Task.FromResult(RepositoryResponse<IEnumerable<TEntity>>.BadRequest(_messages.InvalidRequest));
            }
            try
            {
                var entities = _dbSet.Where(predicate).ToList();
                return Task.FromResult(RepositoryResponse<IEnumerable<TEntity>>.Ok(_messages.EntitiesRetrievedSuccessfully, entities));
            }
            catch
            {
                return Task.FromResult(RepositoryResponse<IEnumerable<TEntity>>.InternalServerError(_messages.ErrorRetrievingEntities));
            }
        }

        public virtual async Task<RepositoryResponse<IEnumerable<TEntity>>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();
                return RepositoryResponse<IEnumerable<TEntity>>.Ok(_messages.EntitiesRetrievedSuccessfully, entities);
            }
            catch
            {
                return RepositoryResponse<IEnumerable<TEntity>>.InternalServerError(_messages.ErrorRetrievingEntities);
            }
        }

        public virtual async Task<RepositoryResponse<TEntity>> GetByIdAsync(TKey id)
        {
            if (id is null)
            {
                return RepositoryResponse<TEntity>.BadRequest(_messages.InvalidRequest);
            }
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity is null)
                {
                    return RepositoryResponse<TEntity>.NotFound(_messages.EntityNotFound);
                }
                return RepositoryResponse<TEntity>.Ok(_messages.EntityRetrievedSuccessfully, entity);
            }
            catch
            {
                return RepositoryResponse<TEntity>.InternalServerError(_messages.ErrorRetrievingEntity);
            }
        }

        public virtual async Task<RepositoryResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            if (entity is null)
            {
                return RepositoryResponse<TEntity>.BadRequest(_messages.InvalidRequest);
            }
            try
            {
                EnsureNotTracked(entity);
                _dbSet.Update(entity);
                await _db.Context.SaveChangesAsync();
                return RepositoryResponse<TEntity>.Ok(_messages.EntityUpdatedSuccessfully, entity);
            }
            catch
            {
                return RepositoryResponse<TEntity>.InternalServerError(_messages.ErrorUpdatingEntity);
            }
        }
    }
}
