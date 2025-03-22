namespace Books_Api.Application.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
}

