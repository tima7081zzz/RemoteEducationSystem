namespace Services.Common.Exceptions;

public class EmptyFieldException : Exception
{
    public EmptyFieldException(string fieldName)
        : base($"Field {fieldName} can not be null")
    {
    }
}