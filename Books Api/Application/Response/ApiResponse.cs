namespace Books_Api.Application.Response;

public class ApiResponse<T>(T data, string message = "", bool success = true)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
    public T Data { get; set; } = data;
}