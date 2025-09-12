using System.Linq.Expressions;

namespace BaseUtility
{
    public interface IService<TDomain, TEntity, TKey>
        where TDomain : class
        where TEntity : class
    {
        Task<BusinessResponse<TDomain>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<BusinessResponse<TDomain>> CreateAsync(TDomain domainModel, CancellationToken cancellationToken = default);
        Task<BusinessResponse<TDomain>> UpdateAsync(TDomain domainModel, CancellationToken cancellationToken = default);
        Task<BusinessResponse<TDomain>> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        Task<BusinessResponse<IEnumerable<TDomain>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BusinessResponse<IEnumerable<TDomain>>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
