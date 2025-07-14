namespace BaseUtility
{
    public class ValidationPipeline<TRequest, TData, TContext>
        where TContext : class
        where TData : class
    {
        private readonly List<IValidationHandler<TRequest, TData, TContext>> _handlers = new();

        public ValidationPipeline<TRequest, TData, TContext> AddHandlers(IEnumerable<IValidationHandler<TRequest, TData, TContext>> handlers)
        {
            _handlers.AddRange(handlers);
            return this;
        }

        public async Task<BusinessResponse<TData>> ValidateAsync(TRequest request, ContextProvider<TContext> context)
        {
            if(_handlers.Count == 0)
            {
                return BusinessResponse<TData>.Ok("No validation required");
            }

            for (int i = 0; i < _handlers.Count - 1; i++)
            {
                _handlers[i].SetNext(_handlers[i + 1]);
            }

            return await _handlers[0].HandleAsync(request, context);
        }
    }
}
