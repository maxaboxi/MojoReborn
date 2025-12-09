namespace Mojo.Shared.Responses;

public abstract class BaseResponse
{
    public bool IsSuccess { get; set; } = true;
    public string? Message { get; set; }
    public bool IsNotFound { get; set; } = false;
    public bool IsNotAuthorized { get; set; } = false;

    public static T NotFound<T>(string message = "Resource not found") where T : BaseResponse, new()
    {
        return new T { IsSuccess = false, IsNotFound = true, Message = message };
    }

    public static T Success<T>(string message = "Operation successful") where T : BaseResponse, new()
    {
        return new T { IsSuccess = true, Message = message };
    }

    public static T Unauthorized<T>(string message = "Unauthorized") where T : BaseResponse, new()
    {
        return new T { IsSuccess = false, IsNotFound = false, IsNotAuthorized = true, Message = message };
    }
}
