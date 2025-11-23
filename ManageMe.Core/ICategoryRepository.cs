namespace ManageMe.Core;

public interface ICategoryRepository
{
    public bool HasCategoryByName(string name, int userId);

    public Category Create(Category category);
}
