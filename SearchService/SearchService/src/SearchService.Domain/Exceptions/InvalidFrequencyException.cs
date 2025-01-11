namespace SearchService.Domain.Exceptions;

public class InvalidFrequencyException : Exception
{
    public InvalidFrequencyException() : base("Invalid frequency")
    {

    }

    public InvalidFrequencyException(string message) : base(message)
    {

    }
}