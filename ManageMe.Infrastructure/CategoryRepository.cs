using ManageMe.Core;

namespace ManageMe.Infrastructure;

public class CategoryRepository(ManageMeContext context) : ICategoryRepository
{
    public Category Create(Category category)
    {
        context.Categories.Add(category);
        context.SaveChanges();
        return category;
    }

    public bool HasCategoryByName(string name, int userId)
    {
        return context.Categories.Any(c => c.Name == name && c.UserId == userId);
    }
}
