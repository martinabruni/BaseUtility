using System.Net;

namespace BaseUtility
{
    public class ApiResponse<TData>
    {
        public TData? Data { get; set; }
        public string? Message { get; set; }
        public required HttpStatusCode StatusCode { get; set; }
    }
}
