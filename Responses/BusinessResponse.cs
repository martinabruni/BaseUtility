namespace BaseUtility
{
    public class BusinessResponse<TData>
        where TData : class
    {
        public TData? Data { get; set; }
        public string? Message { get; set; }
        public required BusinessCode StatusCode { get; set; }

        public static BusinessResponse<TData> BadRequest(string? message = null) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.BadRequest,
            Message = message,
        };

        public static BusinessResponse<TData> Created(string? message = null, TData? data = default) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.Created,
            Message = message,
            Data = data
        };

        public static BusinessResponse<TData> Ok(string? message = null, TData? data = default) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.Ok,
            Message = message,
            Data = data
        };

        public static BusinessResponse<TData> NotFound(string? message = null) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.NotFound,
            Message = message,
        };

        public static BusinessResponse<TData> InternalServerError(string? message = null) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.InternalServerError,
            Message = message,
        };

        public static BusinessResponse<TData> Unauthorize(string? message = null) => new BusinessResponse<TData>
        {
            StatusCode = BusinessCode.Unauthorized,
            Message = message,
        };
    }
}
