using ManageMe.Api.Controllers.DTOs.Output;
using ManageMe.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Filters.Handlers;

public class ApplicationExceptionHandler(IApplicationLogger logger): IExceptionHandler
{
    public IActionResult Handle(Exception exception)
    {
        logger.Error(exception.Message);

        string message = exception.Message;
        return new UnprocessableEntityObjectResult(BaseApiResponse.OnlyMessage(message));
    }
}
