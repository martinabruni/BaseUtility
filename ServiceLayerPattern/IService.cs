using System.Linq.Expressions;

namespace BaseUtility
{
    public interface IService<TDto, TDatabase, TKey>
        where TDto : class, IEntity<TKey>
        where TDatabase : class, IEntity<TKey>
    {
        Task<BusinessResponse<TDto>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<BusinessResponse<TDto>> CreateAsync(TDto dto, CancellationToken cancellationToken = default);
        Task<BusinessResponse<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken = default);
        Task<BusinessResponse<TDto>> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        Task<BusinessResponse<IEnumerable<TDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BusinessResponse<IEnumerable<TDto>>> FindAsync(Expression<Func<TDatabase, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
