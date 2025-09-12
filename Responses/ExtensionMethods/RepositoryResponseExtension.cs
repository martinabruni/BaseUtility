namespace BaseUtility
{
    public static class RepositoryResponseExtension
    {
        public static BusinessResponse<TDomain> ToBusinessResponse<TDomain, TEntity>(
            this RepositoryResponse<TEntity> repositoryResponse,
            IMapper<TEntity, TDomain> mapper)
            where TEntity : class
            where TDomain : class
        {
            return new BusinessResponse<TDomain>
            {
                StatusCode = (BusinessCode)repositoryResponse.StatusCode,
                Message = repositoryResponse.Message,
                Data = repositoryResponse.Data is null ? null : mapper.Map(repositoryResponse.Data)
            };
        }

        public static BusinessResponse<IEnumerable<TDomain>> ToBusinessResponse<TDomain, TEntity>(
            this RepositoryResponse<IEnumerable<TEntity>> repositoryResponse,
            IMapper<TEntity, TDomain> mapper)
            where TEntity : class
            where TDomain : class
        {
            return new BusinessResponse<IEnumerable<TDomain>>
            {
                StatusCode = (BusinessCode)repositoryResponse.StatusCode,
                Message = repositoryResponse.Message,
                Data = repositoryResponse.Data is null
                    ? null
                    : repositoryResponse.Data.Select(mapper.Map).ToList()
            };
        }

        public static BusinessResponse<TData> ToBusinessResponse<TData>(
            this RepositoryResponse<TData> repositoryResponse)
            where TData : class
        {
            return new BusinessResponse<TData>
            {
                StatusCode = (BusinessCode)repositoryResponse.StatusCode,
                Message = repositoryResponse.Message,
                Data = repositoryResponse.Data
            };
        }
    }
}
