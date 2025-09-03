namespace GB.Domain.Errors;

public class EntityAlreadyExistsException(string message) : Exception(message);