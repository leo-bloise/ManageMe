using ManageMe.Api.Controllers.DTOs.Output;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Filters.Handlers;

public class UnauthorizedAccessExceptionHandler : IExceptionHandler
{
    public IActionResult Handle(Exception exception)
    {
        return new UnauthorizedObjectResult(BaseApiResponse.OnlyMessage("Not authorized"));
    }
}
