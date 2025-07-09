using System.Linq.Expressions;

namespace BaseUtility
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
    {
        Task<RepositoryResponse<TEntity>> GetByIdAsync(TKey id);
        Task<RepositoryResponse<TEntity>> CreateAsync(TEntity entity);
        Task<RepositoryResponse<TEntity>> UpdateAsync(TEntity entity);
        Task<RepositoryResponse<TEntity>> DeleteAsync(TKey id);
        Task<RepositoryResponse<IEnumerable<TEntity>>> GetAllAsync();
        Task<RepositoryResponse<IEnumerable<TEntity>>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
