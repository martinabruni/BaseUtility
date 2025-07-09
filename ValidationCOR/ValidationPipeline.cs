namespace BaseUtility
{
    public class ValidationPipeline<TRequest, TData, TKey>
        where TRequest : class
        where TData : class
        where TKey : notnull
    {
        private readonly List<IValidationHandler<TRequest, TData, TKey>> _handlers = new();

        public ValidationPipeline<TRequest, TData, TKey> AddHandler(IValidationHandler<TRequest, TData, TKey> handler)
        {
            _handlers.Add(handler);
            return this;
        }

        public async Task<BusinessResponse<TData>> ValidateAsync(TRequest request, IValidationContext<TData, TKey> context)
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
