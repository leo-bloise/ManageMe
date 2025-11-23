using ManageMe.Api.Controllers.DTOs.Input;
using ManageMe.Application.UseCases;
using ManageMe.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using ManageMe.Core;
using ManageMe.Api.Controllers.DTOs.Output;

namespace ManageMe.Api.Controllers;

public class CategoryController(CreateCategoryUseCase createCategoryUseCase): ManageMeController
{
    [HttpPost]
    public ActionResult Create([FromBody] CreateCategoryInput createCategoryInput)
    {
        Category category = createCategoryUseCase.Execute(new CreateCategory(BuildPrincipal(), createCategoryInput.Name));

        return Ok(BaseApiResponse.WithData("Category created sucessfully", new()
        {
            {"category", new CategoryCreated(category.Id, category.Name) }
        }));
    }
}
