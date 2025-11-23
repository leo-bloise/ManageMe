namespace ManageMe.Application.Exceptions;

public class CategoryAlreadyExistsException : AppException
{
    public CategoryAlreadyExistsException(string name) : base($"Category {name} already exists")
    {
    }
}
