namespace GB.Domain.Errors;

public class BusinessValidationException(string message) : Exception(message)
{
    
}