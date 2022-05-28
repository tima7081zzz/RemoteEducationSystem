namespace Services.Common.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string entityName, object key)
    : base($"Entity {entityName} with key {key} already exists")
    {
    }
}