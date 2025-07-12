namespace BaseUtility
{
    public abstract class BaseValidationHandler<TRequest, TData, TContext> : IValidationHandler<TRequest, TData, TContext>
        where TData : class
        where TContext : class
    {
        private IValidationHandler<TRequest, TData, TContext>? _nextHandler;

        public async Task<BusinessResponse<TData>> HandleAsync(TRequest request, ValidationContext<TContext> context)
        {
            var result = await ValidateAsync(request, context);

            if (result.StatusCode != BusinessCode.Ok)
            {
                return result;
            }

            if (_nextHandler is null)
            {
                return result;
            }

            return await _nextHandler.HandleAsync(request, context);
        }

        public IValidationHandler<TRequest, TData, TContext> SetNext(IValidationHandler<TRequest, TData, TContext> nextHandler)
        {
            _nextHandler = nextHandler;
            return nextHandler;
        }

        protected abstract Task<BusinessResponse<TData>> ValidateAsync(TRequest request, ValidationContext<TContext> context);
    }
}
