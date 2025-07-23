using System.Linq.Expressions;

namespace BaseUtility
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<RepositoryResponse<TEntity>> GetByIdAsync(TKey id, CancellationToken cancellationToken);
        Task<RepositoryResponse<TEntity>> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<RepositoryResponse<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<RepositoryResponse<TEntity>> DeleteAsync(TKey id, CancellationToken cancellationToken);
        Task<RepositoryResponse<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken);
        Task<RepositoryResponse<IEnumerable<TEntity>>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}
