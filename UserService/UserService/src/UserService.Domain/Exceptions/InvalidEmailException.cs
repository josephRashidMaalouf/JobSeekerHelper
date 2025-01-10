namespace UserService.Domain.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException() : base("Invalid email address. Expected format: xxx@xxx.xx")
    {

    }

    public InvalidEmailException(string message) : base(message)
    {

    }
}