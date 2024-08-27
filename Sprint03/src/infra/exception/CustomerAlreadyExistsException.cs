namespace Sprint03.domain.exceptions;

public class CustomerAlreadyExistsException : Exception
{
    public CustomerAlreadyExistsException(string message) : base(message) { }
}