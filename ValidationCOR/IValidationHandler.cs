namespace BaseUtility
{
    public interface IValidationHandler<TRequest, TData, TKey>
        where TRequest : class
        where TData : class
        where TKey : notnull
    {
        Task<BusinessResponse<TData>> HandleAsync(TRequest request, IValidationContext<TData, TKey> context);
        IValidationHandler<TRequest, TData, TKey> SetNext(IValidationHandler<TRequest, TData, TKey> nextHandler);
    }
}
