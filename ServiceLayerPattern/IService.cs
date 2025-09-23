//using System.Linq.Expressions;

//namespace BaseUtility
//{
//    //TODO: Remove
//    public interface IService<TDomain, TKey, TCode>
//        where TDomain : class
//        where TCode : struct, Enum
//    {
//        Task<IResponse<TDomain, TCode>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
//        Task<IResponse<TDomain, TCode>> CreateAsync(TDomain domainModel, CancellationToken cancellationToken = default);
//        Task<IResponse<TDomain, TCode>> UpdateAsync(TDomain domainModel, CancellationToken cancellationToken = default);
//        Task<IResponse<TDomain, TCode>> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
//        Task<IResponse<IEnumerable<TDomain>, TCode>> GetAllAsync(CancellationToken cancellationToken = default);
//        Task<IResponse<IEnumerable<TDomain>, TCode>> FindAsync(Expression<Func<TDomain, bool>> predicate, CancellationToken cancellationToken = default);
//    }
//}
