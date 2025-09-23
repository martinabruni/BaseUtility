namespace BaseUtility
{

    public class RepositoryResponse<TData> : IResponse<TData, RepositoryCode>
    {
        public TData? Data { get; set; }
        public string? Message { get; set; }
        public required RepositoryCode StatusCode { get; set; }

        public static RepositoryResponse<TData> BadRequest(string? message = null) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.BadRequest,
            Message = message,
        };

        public static RepositoryResponse<TData> Created(TData? data, string? message = null) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Created,
            Message = message,
            Data = data
        };

        public static RepositoryResponse<TData> Ok(TData? data, string? message = null) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Ok,
            Message = message,
            Data = data
        };

        public static RepositoryResponse<TData> Ok(string? message = null) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Ok,
            Message = message,
        };

        public static RepositoryResponse<TData> NotFound(string? message = null) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.NotFound,
            Message = message,
        };

        public static RepositoryResponse<TData> InternalServerError(string? message = null) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.InternalServerError,
            Message = message,
        };

        public static RepositoryResponse<TData> Unauthorize(string? message = null) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.Unauthorized,
            Message = message,
        };

        public static RepositoryResponse<TData> NotImplemented(string? message = null) => new RepositoryResponse<TData>
        {
            StatusCode = RepositoryCode.NotImplemented,
            Message = message,
        };
    }
}
