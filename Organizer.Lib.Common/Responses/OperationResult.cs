namespace Organizer.Lib.Common.Responses;

public class OperationResult<T> : OperationResult where T : class
{
    public T Content { get; set; }

    public static OperationResult<T> Ok(string message)
    {
        return new()
        {
            Message = message,
            IsSuccess = true
        };
    }

    public static OperationResult<T> Failure(string message)
    {
        return new()
        {
            Message = message,
            IsSuccess = false
        };
    }

    public static OperationResult<T> Get(T content)
    {
        return new()
        {
            Content = content,
            IsSuccess = true
        };
    }
}

public class OperationResult
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }

    public static OperationResult Ok()
    {
        return new()
        {
            IsSuccess = true
        };
    }

    public static OperationResult Failure(string message)
    {
        return new()
        {
            Message = message,
            IsSuccess = false
        };
    }
}
