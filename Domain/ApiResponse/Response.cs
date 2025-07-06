using System.Net;

namespace Domain.ApiResponse;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public string? Massage { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    public Response(T? data, string? massage = null)
    {
        IsSuccess = true;
        Massage = massage;
        Data = data;
        StatusCode = (int)HttpStatusCode.OK;
    }

    public Response(string massage, HttpStatusCode statusCode)
    {
        IsSuccess = false;
        Massage = massage;
        Data = default;
        StatusCode = (int)statusCode;
    }
}
