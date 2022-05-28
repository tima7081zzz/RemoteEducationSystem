namespace Services.Common.Exceptions;

public class WrongOperationException: Exception
{
    public WrongOperationException(string entityName, object key) : base($"This is impossible operation for entity {entityName} because of key {key}") { }
}