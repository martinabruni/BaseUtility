using System.Linq.Expressions;

namespace BaseUtility
{
    public interface IService<TDto, TDatabase, TKey>
        where TDto : class, IEntity<TKey>
        where TDatabase : class, IEntity<TKey>
    {
        Task<BusinessResponse<TDto>> GetByIdAsync(TKey id);
        Task<BusinessResponse<TDto>> CreateAsync(TDto dto);
        Task<BusinessResponse<TDto>> UpdateAsync(TDto dto);
        Task<BusinessResponse<TDto>> DeleteAsync(TKey id);
        Task<BusinessResponse<IEnumerable<TDto>>> GetAllAsync();
        Task<BusinessResponse<IEnumerable<TDto>>> FindAsync(Expression<Func<TDatabase, bool>> predicate);
    }
}
