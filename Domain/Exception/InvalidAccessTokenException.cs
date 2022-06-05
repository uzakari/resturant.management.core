namespace Domain.Exception;

public class InvalidAccessTokenException : ApplicationException
{
    public InvalidAccessTokenException(string message) : base(message)
    {

    }
}
