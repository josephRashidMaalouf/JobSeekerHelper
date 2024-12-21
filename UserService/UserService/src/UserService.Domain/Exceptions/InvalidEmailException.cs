namespace UserService.Domain.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException() : base("Invalid email address.")
    {
        
    }

    public InvalidEmailException(string message) : base(message)
    {
        
    }
}