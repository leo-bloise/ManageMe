using ManageMe.Application.DTOs;
using ManageMe.Application.Exceptions;
using ManageMe.Core;

namespace ManageMe.Application.UseCases;

public class CreateCategoryUseCase(ICategoryRepository categoryRepository)
{
    public Category Execute(CreateCategory data)
    {
        Category category = new Category(0, data.Name, data.Principal.Id);

        if(categoryRepository.HasCategoryByName(category.Name, category.UserId))
        {
            throw new CategoryAlreadyExistsException(category.Name);
        }

        categoryRepository.Create(category);

        return category;
    }
}
