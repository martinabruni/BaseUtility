using System.Net;

namespace BaseUtility
{
    public class ApiResponse<TData> : IResponse<TData, HttpStatusCode>
    {
        public TData? Data { get; set; }
        public string? Message { get; set; }
        public required HttpStatusCode StatusCode { get; set; }
    }
}
