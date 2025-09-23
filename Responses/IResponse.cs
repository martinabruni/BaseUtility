namespace BaseUtility
{
    public interface IResponse<TData, TCode>
        where TCode : struct, Enum
    {
        TData? Data { get; set; }
        string? Message { get; set; }
        TCode StatusCode { get; set; }
        bool Failed => Convert.ToInt32(StatusCode) >= 400;
    }
}