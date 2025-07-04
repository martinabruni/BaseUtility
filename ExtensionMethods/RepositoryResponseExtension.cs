namespace BaseUtility
{
    public static class RepositoryResponseExtension
    {
        public static BusinessResponse<TDto> ToBusinessResponse<TDto, TDatabase>(
            this RepositoryResponse<TDatabase> repositoryResponse,
            IMapper<TDatabase, TDto> mapper)
            where TDatabase : class
            where TDto : class
        {
            return new BusinessResponse<TDto>
            {
                StatusCode = (BusinessCode)repositoryResponse.StatusCode,
                Message = repositoryResponse.Message,
                Data = repositoryResponse.Data is null ? null : mapper.Map(repositoryResponse.Data)
            };
        }

        public static BusinessResponse<IEnumerable<TDto>> ToBusinessResponse<TDto, TDatabase>(
            this RepositoryResponse<IEnumerable<TDatabase>> repositoryResponse,
            IMapper<TDatabase, TDto> mapper)
            where TDatabase : class
            where TDto : class
        {
            return new BusinessResponse<IEnumerable<TDto>>
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
