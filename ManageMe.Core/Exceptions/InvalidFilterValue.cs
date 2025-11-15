namespace ManageMe.Core.Exceptions;

public class InvalidFilterValue : Exception
{
    public InvalidFilterValue(string field, string reason) : base($"Invalid data for {field} filter. {reason}")
    {
    }
}
