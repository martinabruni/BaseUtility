using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BaseUtility
{
    public abstract class BaseRepository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TContext : DbContext
    {
        protected readonly IDatabase<TContext> _db;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ResponseMessage _messages;

        public BaseRepository(IDatabase<TContext> db, ResponseMessage messages)
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
                return RepositoryResponse<TEntity>.Created(entity, _messages.EntityCreatedSuccessfully);
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
                return RepositoryResponse<TEntity>.Ok(entity, _messages.EntityDeletedSuccessfully);
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
                return Task.FromResult(RepositoryResponse<IEnumerable<TEntity>>.Ok(entities, _messages.EntitiesRetrievedSuccessfully));
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
                return RepositoryResponse<IEnumerable<TEntity>>.Ok(entities, _messages.EntitiesRetrievedSuccessfully);
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
                return RepositoryResponse<TEntity>.Ok(entity, _messages.EntityRetrievedSuccessfully);
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
                return RepositoryResponse<TEntity>.Ok(entity, _messages.EntityUpdatedSuccessfully);
            }
            catch
            {
                return RepositoryResponse<TEntity>.InternalServerError(_messages.ErrorUpdatingEntity);
            }
        }
    }
}
