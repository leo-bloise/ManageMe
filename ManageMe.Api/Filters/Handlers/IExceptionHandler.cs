using ManageMe.Api.Controllers.DTOs.Output;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Filters.Handlers;

public interface IExceptionHandler
{
    public IActionResult Handle(Exception exception);
}
