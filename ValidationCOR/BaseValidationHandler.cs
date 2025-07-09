namespace BaseUtility
{
    public abstract class BaseValidationHandler<TRequest, TData, TKey> : IValidationHandler<TRequest, TData, TKey>
        where TRequest : class
        where TData : class
        where TKey : notnull
    {
        private IValidationHandler<TRequest, TData, TKey>? _nextHandler;

        public async Task<BusinessResponse<TData>> HandleAsync(TRequest request, IValidationContext<TData, TKey> context)
        {
            var result = await ValidateAsync(request, context);

            if (result.Data is null)
            {
                return result;
            }

            if (_nextHandler is null)
            {
                return result;
            }

            return await _nextHandler.HandleAsync(request, context);
        }

        public IValidationHandler<TRequest, TData, TKey> SetNext(IValidationHandler<TRequest, TData, TKey> nextHandler)
        {
            _nextHandler = nextHandler;
            return nextHandler;
        }

        protected abstract Task<BusinessResponse<TData>> ValidateAsync(TRequest request, IValidationContext<TData, TKey> context);
    }
}
