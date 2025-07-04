namespace BaseUtility
{
    public static class RepositoryResponseExtension
    {
        public static BusinessResponse<TDto> ToBusinessResponse<TDto, TDatabase>(this RepositoryResponse<TDatabase> repositoryResponse)
            where TDatabase : class
            where TDto : class
        {
            return new BusinessResponse<TDto>
            {
                Message = repositoryResponse.Message,
                StatusCode = (BusinessCode) repositoryResponse.StatusCode
            };
        }
        public static BusinessResponse<TData> ToBusinessResponse<TData>(this RepositoryResponse<TData> repositoryResponse)
            where TData : class
        {
            return new BusinessResponse<TData>
            {
                Message = repositoryResponse.Message,
                StatusCode = (BusinessCode)repositoryResponse.StatusCode
            };
        }
    }
}
