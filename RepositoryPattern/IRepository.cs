using System.Linq.Expressions;

namespace BaseUtility
{
    public interface IRepository<TEntity, TKey, TCode>
        where TEntity : class
        where TCode : struct, Enum
    {
        Task<IResponse<TEntity, TCode>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<IResponse<TEntity, TCode>> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<IResponse<TEntity, TCode>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<IResponse<TEntity, TCode>> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        Task<IResponse<IEnumerable<TEntity>, TCode>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IResponse<IEnumerable<TEntity>, TCode>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
