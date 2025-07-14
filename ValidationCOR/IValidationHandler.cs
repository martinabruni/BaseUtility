namespace BaseUtility
{
    public interface IValidationHandler<TRequest, TData, TContext>
        where TData : class
        where TContext : class
    {
        Task<BusinessResponse<TData>> HandleAsync(TRequest request, ContextProvider<TContext> context);
        IValidationHandler<TRequest, TData, TContext> SetNext(IValidationHandler<TRequest, TData, TContext> nextHandler);
    }
}
