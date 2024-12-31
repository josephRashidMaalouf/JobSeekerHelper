namespace UserService.Domain.Models;

public class Result<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }

    public int Code { get; set; }

    private Result()
    {

    }

    public static Result<T> Success(T data)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            Code = 200,
        };
    }

    public static Result<T> Failure(string errorMessage, int code)
    {
        return new Result<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Code = code,
        };
    }
}