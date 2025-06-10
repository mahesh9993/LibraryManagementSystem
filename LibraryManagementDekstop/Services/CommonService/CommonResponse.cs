using Grpc.Core;

public class CommonResponse
{
    public StatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }

    public CommonResponse(StatusCode statusCode, string message, object data)
    {
        this.StatusCode = statusCode;
        this.Message = message;
        this.Data = data;
    }
}
